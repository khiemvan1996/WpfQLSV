using System.Windows;
using WpfQLSV.ViewModels;
using WpfQLSV.Models;

namespace WpfQLSV.Views
{
    public partial class EditClassWindow : Window
    {
        public EditClassWindow(ClassesViewModel classesViewModel, Class classItem)
        {
            InitializeComponent();
            DataContext = new EditClassViewModel(classesViewModel, classItem);
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
