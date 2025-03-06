using System.Windows.Controls;
using WpfQLSV.ViewModels;

namespace WpfQLSV.Views
{
    public partial class ScoresView : UserControl
    {
        public ScoresView(ScoresViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }

}
