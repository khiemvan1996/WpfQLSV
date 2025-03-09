using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfQLSV.Models;
using WpfQLSV.Views;

namespace WpfQLSV.ViewModels
{
    public partial class ClassesViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Class> _classes;

        [ObservableProperty]
        private Class _selectedClass;

        public ICommand AddClassCommand { get; }
        public ICommand DeleteClassCommand { get; }
        public ICommand EditClassCommand { get; }

        public ClassesViewModel()
        {
            Classes = new ObservableCollection<Class>();
            LoadClasses();

            AddClassCommand = new RelayCommand(OpenAddClassWindow);
            DeleteClassCommand = new RelayCommand(DeleteClass, CanDeleteClass);
            EditClassCommand = new RelayCommand(OpenEditClassWindow, CanEdit);
        }

        private void OpenAddClassWindow()
        {
            var addClassWindow = new AddClassWindow(this);
            addClassWindow.ShowDialog();
        }

        private void OpenEditClassWindow()
        {
            if (SelectedClass == null)
            {
                MessageBox.Show("Vui lòng chọn một lớp để chỉnh sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new EditClassWindow(this, SelectedClass);
            if (editWindow.ShowDialog() == true)
            {
                using var db = new StudentMngContext();
                var classData = db.Classes.Find(SelectedClass.Id);
                if (classData != null)
                {
                    classData.ClassName = SelectedClass.ClassName;
                    db.SaveChanges();
                }
                LoadClasses();
            }
        }

        private bool CanEdit() => SelectedClass != null;
        private bool CanDeleteClass() => SelectedClass != null;

        private void DeleteClass()
        {
            if (SelectedClass != null)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp này?",
                    "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using var context = new StudentMngContext();
                    context.Classes.Remove(SelectedClass);
                    context.SaveChanges();
                    Classes.Remove(SelectedClass);
                }
            }
        }

        public void LoadClasses()
        {
            using var db = new StudentMngContext();
            Classes = new ObservableCollection<Class>(
                db.Classes.Include(c => c.Students).ToList()
            );
        }
    }
}
