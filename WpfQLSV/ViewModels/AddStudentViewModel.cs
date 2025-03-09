using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfQLSV.Models;

//using WpfQLSV.Model;
using WpfQLSV.ViewModels;
using WpfQLSV.Views;

namespace WpfQLSV.ViewModels
{
    public partial class AddStudentViewModel : ObservableObject
    {
        private readonly StudentsViewModel _studentsViewModel;

        [ObservableProperty]
        private ObservableCollection<Class> classList;

        [ObservableProperty]
        private Class selectedClass;

        [ObservableProperty]
        private string studentName; // Tên sinh viên

        [ObservableProperty]
        private DateTime dateOfBirth = DateTime.Today;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddStudentViewModel(StudentsViewModel studentsViewModel)
        {
            _studentsViewModel = studentsViewModel;
            //ClassList = new ObservableCollection<Class>();
            LoadClasses();

            SaveCommand = new RelayCommand(SaveStudent);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void LoadClasses()
        {
            using (var context = new StudentMngContext())
            {
                var classes = context.Classes.ToList();
                if (classes.Any())
                {
                    ClassList = new ObservableCollection<Class>(classes);
                    SelectedClass = ClassList.First();
                }
            }
        }

        private void SaveStudent()
        {
            if (string.IsNullOrWhiteSpace(StudentName) || SelectedClass == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new StudentMngContext())
            {
                var newStudent = new Student
                {
                    FullName = StudentName,
                    DateOfBirth = DateOfBirth, // Chuyển đổi từ DateTime sang DateOnly
                    IdClass = SelectedClass.Id
                };

                context.Students.Add(newStudent);
                context.SaveChanges();
            }

            _studentsViewModel.LoadStudents(); // Cập nhật danh sách
            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            // CloseWindow();  
        }

        private void Cancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is AddStudentWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }

}