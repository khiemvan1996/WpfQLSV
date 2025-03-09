using System.Windows;
using WpfQLSV.ViewModels;
namespace WpfQLSV.Views
{
    public partial class AddSubjectWindow : Window
    {

        public AddSubjectWindow(SubjectsViewModel subjectsViewModel)
        {
            InitializeComponent();
            DataContext = new AddSubjectViewModel(subjectsViewModel);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
