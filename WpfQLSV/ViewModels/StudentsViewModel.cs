using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfQLSV.Views;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Linq;
using WpfQLSV.Services;
using ClosedXML.Excel;
using WpfQLSV.Models;
using System.Windows.Controls;
using System.Windows.Data;

public partial class StudentsViewModel : ObservableObject
{
    private readonly IFileDialogService _fileDialogService;
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private ObservableCollection<Student> students;

    [ObservableProperty]
    private ObservableCollection<Student> filteredStudents;

    [ObservableProperty]
    private string searchText;

    [ObservableProperty]
    private Student selectedStudent;



    public ICommand AddStudentCommand { get; }
    public ICommand SearchCommand { get; }
    public ICommand DeleteStudentCommand { get; }
    public ICommand EditStudentCommand { get; }
    public ICommand ImportFromExcelCommand { get; }
    public ICommand ExportToExcelCommand { get; }

    public StudentsViewModel(IFileDialogService fileDialogService, IMessageService messageService)
    {
        _fileDialogService = fileDialogService;
        _messageService = messageService;

        Students = new ObservableCollection<Student>();
        FilteredStudents = new ObservableCollection<Student>();
        LoadStudents();
        AddStudentCommand = new RelayCommand(OpenAddStudentWindow);
        SearchCommand = new RelayCommand(SearchStudents);
        DeleteStudentCommand = new RelayCommand(DeleteStudent);
        EditStudentCommand = new RelayCommand(EditStudent);
        ImportFromExcelCommand = new RelayCommand(ImportFromExcel);
        ExportToExcelCommand = new RelayCommand(ExportToExcel);
    }
    private void ImportFromExcel()
    {
        try
        {
            var filePath = _fileDialogService.OpenFileDialog("Excel Files|*.xlsx;*.xls", "Chọn file Excel để nhập dữ liệu");

            if (!string.IsNullOrEmpty(filePath))
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // Bỏ qua hàng tiêu đề

                    using (var context = new StudentMngContext())
                    {
                        foreach (var row in rows)
                        {
                            // Đọc dữ liệu từ Excel
                            var fullName = row.Cell(1).GetValue<string>();
                            var dateOfBirth = row.Cell(2).GetValue<DateTime>();
                            var idClassString = row.Cell(3).GetValue<string>(); // Đọc giá trị dưới dạng chuỗi

                            // Kiểm tra xem IdClass có phải là số nguyên hợp lệ không
                            if (!int.TryParse(idClassString, out int idClass))
                            {
                                _messageService.ShowError($"Dữ liệu không hợp lệ tại dòng {row.RowNumber()}: IdClass phải là một số nguyên.", "Lỗi");
                                continue; // Bỏ qua dòng này và tiếp tục với dòng tiếp theo
                            }

                            // Kiểm tra dữ liệu hợp lệ
                            if (string.IsNullOrEmpty(fullName) || idClass <= 0)
                            {
                                _messageService.ShowError($"Dữ liệu không hợp lệ tại dòng {row.RowNumber()}: Tên không được trống và IdClass phải lớn hơn 0.", "Lỗi");
                                continue; // Bỏ qua dòng này và tiếp tục với dòng tiếp theo
                            }

                            // Kiểm tra xem IdClass có tồn tại trong database không
                            var classExists = context.Classes.Any(c => c.Id == idClass);
                            if (!classExists)
                            {
                                _messageService.ShowError($"Dữ liệu không hợp lệ tại dòng {row.RowNumber()}: IdClass không tồn tại trong database.", "Lỗi");
                                continue; // Bỏ qua dòng này và tiếp tục với dòng tiếp theo
                            }

                            // Thêm dữ liệu vào database
                            var student = new Student
                            {
                                FullName = fullName,
                                DateOfBirth = dateOfBirth,
                                IdClass = idClass,
                            };

                            context.Students.Add(student);
                        }

                        context.SaveChanges();
                    }
                }

                LoadStudents(); // Cập nhật danh sách sinh viên sau khi nhập
                _messageService.ShowMessage("Nhập dữ liệu từ Excel thành công!", "Thông báo");
            }
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Lỗi khi nhập dữ liệu từ Excel: {ex.Message}", "Lỗi");
        }
    }// done
    private void ExportToExcel()
    {
        try
        {
            var filePath = _fileDialogService.SaveFileDialog("Excel Files|*.xlsx", "Lưu file Excel", "Students.xlsx");

            if (!string.IsNullOrEmpty(filePath))
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Students");

                    // Thêm header
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Họ và Tên";
                    worksheet.Cell(1, 3).Value = "Ngày Sinh";
                    worksheet.Cell(1, 4).Value = "Lớp";
                    worksheet.Cell(1, 5).Value = "Môn Học Đã Đăng Ký";

                    int row = 2;
                    foreach (var student in FilteredStudents)
                    {
                        worksheet.Cell(row, 1).Value = student.Id;
                        worksheet.Cell(row, 2).Value = student.FullName;
                        worksheet.Cell(row, 3).Value = student.DateOfBirth.ToString("d") ?? string.Empty;
                        worksheet.Cell(row, 4).Value = student.IdClassNavigation?.ClassName ?? string.Empty;

                        // Lấy danh sách môn học từ Scores
                        var subjects = student.Scores?
                            .Select(s => s.Subject?.SubjectName)
                            .Where(name => !string.IsNullOrEmpty(name))
                            .Distinct() ?? Enumerable.Empty<string>();
                        worksheet.Cell(row, 5).Value = string.Join(", ", subjects);

                        row++;
                    }

                    workbook.SaveAs(filePath);
                }

                _messageService.ShowMessage("Xuất dữ liệu ra Excel thành công!", "Thông báo");
            }
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Lỗi khi xuất dữ liệu ra Excel: {ex.Message}", "Lỗi");
        }
    }



    // done
    
    public void LoadStudents()
    {
        using (var context = new StudentMngContext())
        {
            var studentList = context.Students
                .Include(s => s.IdClassNavigation)  // Sửa ở đây
                .Include(s => s.Scores)
                .ThenInclude(sc => sc.Subject)
                .ToList();

            Students = new ObservableCollection<Student>(studentList);
            FilteredStudents = new ObservableCollection<Student>(studentList);
        }
  
    } //done

    private void OpenAddStudentWindow()
    {
        var addStudentWindow = new AddStudentWindow(this);
        addStudentWindow.ShowDialog();
    }

    private void SearchStudents()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredStudents = new ObservableCollection<Student>(Students);
        }
        else
        {
            var searchTextLower = SearchText.ToLower();
            var filtered = Students.Where(s => s.FullName.ToLower().Contains(searchTextLower)).ToList();
            FilteredStudents = new ObservableCollection<Student>(filtered);
        }
    } //done

    private void DeleteStudent()//done
    {
        if (SelectedStudent != null)
        {
            var result = MessageBox.Show(
             $"Bạn có chắc chắn muốn xóa {SelectedStudent.FullName}?",
            "Xác nhận xóa",
             MessageBoxButton.YesNo,
             MessageBoxImage.Warning);
            // Xóa sinh viên khỏi cơ sở dữ liệu
            if (result == MessageBoxResult.Yes)
            {
                using (var context = new StudentMngContext())
                {
                    var studentToDelete = context.Students.Find(SelectedStudent.Id);
                    if (studentToDelete != null)
                    {
                        context.Students.Remove(studentToDelete);
                        context.SaveChanges();
                    }
                }

                // Xóa sinh viên khỏi danh sách hiển thị
                Students.Remove(SelectedStudent);
                FilteredStudents.Remove(SelectedStudent);
            }
        }
    }

    private void EditStudent() //done
    {
        if (SelectedStudent != null)
        {
            // Mở cửa sổ chỉnh sửa sinh viên
            var editStudentWindow = new EditStudentWindow(SelectedStudent);
            editStudentWindow.ShowDialog();

            // Sau khi chỉnh sửa, cập nhật danh sách
            LoadStudents();
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        SearchStudents();
    }
}