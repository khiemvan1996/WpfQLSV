using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfQLSV.Models;
using WpfQLSV.Views;

namespace WpfQLSV.ViewModels
{
    public partial class EditClassViewModel : ObservableObject
    {
        private readonly ClassesViewModel _classesViewModel;
        private readonly Class _originalClass;

        [ObservableProperty]
        private string className;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditClassViewModel(ClassesViewModel classesViewModel, Class classToEdit)
        {
            _classesViewModel = classesViewModel ?? throw new ArgumentNullException(nameof(classesViewModel));
            _originalClass = classToEdit ?? throw new ArgumentNullException(nameof(classToEdit));

            // Gán dữ liệu từ lớp hiện có
            ClassName = classToEdit.ClassName;

            SaveCommand = new RelayCommand(SaveClass);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SaveClass()
        {
            if (string.IsNullOrWhiteSpace(ClassName))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new StudentMngContext())
            {
                var existingClass = context.Classes.Find(_originalClass.Id);
                if (existingClass != null)
                {
                    existingClass.ClassName = ClassName;
                    context.SaveChanges();
                }
            }

            _classesViewModel.LoadClasses(); // Load lại danh sách lớp
            CloseWindow();
        }

        private void Cancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            Window window = Application.Current.Windows.OfType<EditClassWindow>().FirstOrDefault();
            window?.Close();
        }
    }
}
