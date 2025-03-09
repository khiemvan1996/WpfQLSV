using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfQLSV.Models;
using WpfQLSV.Views;

namespace WpfQLSV.ViewModels
{
    public partial class AddSubjectViewModel : ObservableObject
    {
        private readonly SubjectsViewModel _subjectsViewModel;

        [ObservableProperty]
        private string subjectName;

        [ObservableProperty]
        private int credit = 1; // Đặt giá trị mặc định tránh giá trị 0

        public ObservableCollection<int> CreditList { get; } = new ObservableCollection<int> { 1, 2, 3, 4, 5 };

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddSubjectViewModel(SubjectsViewModel subjectsViewModel)
        {
            _subjectsViewModel = subjectsViewModel ?? throw new ArgumentNullException(nameof(subjectsViewModel));
            SaveCommand = new RelayCommand(SaveSubject);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SaveSubject()
        {
            if (string.IsNullOrWhiteSpace(SubjectName))
            {
                MessageBox.Show("Vui lòng nhập tên môn học!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Lưu môn học mới vào cơ sở dữ liệu
            using (var context = new StudentMngContext())
            {
                var newSubject = new Subject
                {
                    SubjectName = SubjectName, // Dùng SubjectName theo định nghĩa trong Model
                    Credit = Credit
                };

                context.Subjects.Add(newSubject);
                context.SaveChanges();
            }

            // Cập nhật lại danh sách môn học
            _subjectsViewModel.LoadSubjects();
            CloseWindow();
        }

        private void Cancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            // Tìm cửa sổ AddSubjectWindow và đóng nó
            Window window = Application.Current.Windows.OfType<AddSubjectWindow>().FirstOrDefault();
            window?.Close();
        }
    }
}
