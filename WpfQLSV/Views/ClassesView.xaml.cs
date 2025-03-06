using System.Windows.Controls;
using WpfQLSV.ViewModels;

namespace WpfQLSV.Views
{
    public partial class ClassesView : UserControl
    {
        public ClassesView(ClassesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }

}
