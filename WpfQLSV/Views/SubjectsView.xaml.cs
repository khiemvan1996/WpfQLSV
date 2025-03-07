using System.Windows.Controls;
using WpfQLSV.ViewModels;

namespace WpfQLSV.Views
{
    public partial class SubjectsView : UserControl
    {
        public SubjectsView(SubjectsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; // Đặt DataContext là ViewModel được truyền vào
        }
    }
}