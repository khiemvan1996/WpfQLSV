using ClosedXML.Parser;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfQLSV.Models;
using WpfQLSV.Views;

namespace WpfQLSV.ViewModels
{
    public partial class SubjectsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Subject> _subjects;

        [ObservableProperty]
        private Subject _selectedSubject;

        public ICommand AddSubjectCommand { get; }
        public ICommand DeleteSubjectCommand { get; }
        public ICommand EditSubjectCommand { get; }
        public SubjectsViewModel()
        {
            Subjects = new ObservableCollection<Subject>();
            LoadSubjects();

            AddSubjectCommand = new RelayCommand(OpenAddSubjectWindow);
            DeleteSubjectCommand = new RelayCommand(DeleteSubject, CanDeleteSubject);
            EditSubjectCommand = new RelayCommand(OpenEditSubjectWindow, CanEdit);

        }

        private void OpenAddSubjectWindow()
        {
            var addSubjectWindow = new AddSubjectWindow(this);
            addSubjectWindow.ShowDialog();
        }
        private void OpenEditSubjectWindow()
        {
            if (SelectedSubject == null)
            {
                MessageBox.Show("Vui lòng chọn một môn học để chỉnh sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // ✅ Truyền thêm `this` vào constructor
            var editWindow = new EditSubjectWindow(this, SelectedSubject);

            if (editWindow.ShowDialog() == true)
            {
                using var db = new StudentMngContext();
                var subject = db.Subjects.Find(SelectedSubject.Id);
                if (subject != null)
                {
                    subject.SubjectName = SelectedSubject.SubjectName;
                    subject.Credit = SelectedSubject.Credit;

                    db.SaveChanges(); // 🚀 Lưu thay đổi vào database
                }

                LoadSubjects(); // 🔄 Cập nhật UI
            }
        }



        private bool CanEdit() => SelectedSubject != null;
        private bool CanDeleteSubject() => SelectedSubject != null;

        private void DeleteSubject()
        {
            if (SelectedSubject != null)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này?",
                    "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new StudentMngContext())
                    {
                        context.Subjects.Remove(SelectedSubject);
                        context.SaveChanges();
                    }
                    Subjects.Remove(SelectedSubject);
                }
            }
        }

        public void LoadSubjects()
        {
            using var db = new StudentMngContext(); // Thay bằng DbContext của bạn
            Subjects = new ObservableCollection<Subject>(
                db.Subjects.Include(s => s.StudentsSubjects).ToList()
            );
        }
    }
}
