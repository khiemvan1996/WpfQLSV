using System.Windows;
using WpfQLSV.ViewModels;
namespace WpfQLSV.Views
{
    public partial class AddStudentWindow : Window
    {
        private StudentsViewModel _studentsViewModel;

        public AddStudentWindow(StudentsViewModel studentsViewModel)
        {
            InitializeComponent();
            DataContext = new AddStudentViewModel(studentsViewModel);
            // Gán ViewModel làm DataContext
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
