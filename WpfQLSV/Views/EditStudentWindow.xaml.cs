using System.Windows;
using WpfQLSV.Models;
using WpfQLSV.ViewModels;

namespace WpfQLSV.Views
{
    public partial class EditStudentWindow : Window
    {
        public EditStudentWindow(Student student)
        {
            InitializeComponent();
            DataContext = new EditStudentViewModel(student);

        }
    }
}