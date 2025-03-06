using System.Windows.Controls;
using WpfQLSV.ViewModels;

namespace WpfQLSV.Views
{
    public partial class StudentsView : UserControl
    {
        public StudentsView(StudentsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}