using System.Windows;
using WpfQLSV.ViewModels;
namespace WpfQLSV.Views
{
    public partial class AddClassWindow : Window
    {

        public AddClassWindow(ClassesViewModel classesViewModel)
        {
            InitializeComponent();
            DataContext = new AddClassViewModel(classesViewModel);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
