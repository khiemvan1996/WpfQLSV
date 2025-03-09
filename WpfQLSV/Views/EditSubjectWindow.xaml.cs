using System.Windows;
using WpfQLSV.ViewModels;
using WpfQLSV.Models;
namespace WpfQLSV.Views
{
    public partial class EditSubjectWindow : Window
    {
        public EditSubjectWindow(SubjectsViewModel subjectsViewModel, Subject subject)
        {
            InitializeComponent();
            DataContext = new EditSubjectViewModel(subjectsViewModel, subject);
        }
    

    private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
