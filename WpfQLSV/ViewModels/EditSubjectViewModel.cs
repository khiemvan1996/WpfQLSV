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
    public partial class EditSubjectViewModel : ObservableObject
    {
        private readonly SubjectsViewModel _subjectsViewModel;
        private readonly Subject _originalSubject;

        [ObservableProperty]
        private string subjectName;

        [ObservableProperty]
        private int credit;

        public ObservableCollection<int> CreditList { get; } = new ObservableCollection<int> { 1, 2, 3, 4, 5 };

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditSubjectViewModel(SubjectsViewModel subjectsViewModel, Subject subject)
        {
            _subjectsViewModel = subjectsViewModel ?? throw new ArgumentNullException(nameof(subjectsViewModel));
            _originalSubject = subject ?? throw new ArgumentNullException(nameof(subject));

            // Gán dữ liệu từ Subject hiện có
            SubjectName = subject.SubjectName;
            Credit = subject.Credit;

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

            using (var context = new StudentMngContext())
            {
                var existingSubject = context.Subjects.Find(_originalSubject.Id);
                if (existingSubject != null)
                {
                    existingSubject.SubjectName = SubjectName;
                    existingSubject.Credit = Credit;
                    context.SaveChanges();
                }
            }

            _subjectsViewModel.LoadSubjects();
            CloseWindow();
        }

        private void Cancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            Window window = Application.Current.Windows.OfType<EditSubjectWindow>().FirstOrDefault();
            window?.Close();
        }
    }
}
