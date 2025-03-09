using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows;
using WpfQLSV.Models;

namespace WpfQLSV.ViewModels
{
    public partial class AddClassViewModel : ObservableObject
    {
        private readonly ClassesViewModel _classesViewModel;

        [ObservableProperty]
        private Class _newClass;

        public IRelayCommand SaveCommand { get; }
        public IRelayCommand CancelCommand { get; }

        public AddClassViewModel(ClassesViewModel classesViewModel)
        {
            _classesViewModel = classesViewModel ?? throw new ArgumentNullException(nameof(classesViewModel));

            NewClass = new Class
            {
                ClassName = string.Empty,
                Students = new List<Student>()
            };

            SaveCommand = new RelayCommand(SaveClass);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SaveClass()
        {
            if (string.IsNullOrWhiteSpace(NewClass.ClassName))
            {
                MessageBox.Show("Tên lớp không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new StudentMngContext())
            {
                db.Classes.Add(NewClass);
                db.SaveChanges();
            }

            _classesViewModel.LoadClasses(); // Cập nhật danh sách lớp
            MessageBox.Show("Thêm lớp thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            Application.Current.Windows[1]?.Close();
        }

        private void Cancel()
        {
            Application.Current.Windows[1]?.Close();
        }
    }
}
