using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfQLSV.Models;
namespace WpfQLSV.ViewModels;
public partial class EditStudentViewModel : ObservableObject
{
    [ObservableProperty]
    private string studentName;

    [ObservableProperty]
    private DateTime dateOfBirth;

    [ObservableProperty]
    private Class selectedClass;

    [ObservableProperty]
    private ObservableCollection<Class> classList;

    private Student studentToEdit;

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public EditStudentViewModel(Student student)
    {
        studentToEdit = student;
        StudentName = student.FullName;
        DateOfBirth = student.DateOfBirth;

        LoadClasses(); // Load danh sách lớp

        // Sử dụng IdClassNavigation để lấy lớp học hiện tại của sinh viên
        SelectedClass = ClassList.FirstOrDefault(c => c.Id == student.IdClass);

        SaveCommand = new RelayCommand(Save);
        CancelCommand = new RelayCommand(Cancel);
    }

    private void LoadClasses()
    {
        using (var context = new StudentMngContext())
        {
            var classes = context.Classes.ToList();
            ClassList = new ObservableCollection<Class>(classes);
        }
    }

    private void Save()
    {
        using (var context = new StudentMngContext())
        {
            var student = context.Students.Find(studentToEdit.Id);
            if (student != null)
            {
                student.FullName = StudentName;
                student.DateOfBirth = DateOfBirth;
                student.IdClass = SelectedClass.Id;
                context.SaveChanges();
            }
        }

        MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        CloseWindow();
    }

    private void Cancel()
    {
        CloseWindow();
    }

    private void CloseWindow()
    {
        foreach (Window window in Application.Current.Windows)
        {
            if (window.DataContext == this)
            {
                window.Close();
                break;
            }
        }
    }
}