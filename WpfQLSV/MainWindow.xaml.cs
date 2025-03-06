using System.Windows;
using WpfQLSV.ViewModels;
using WpfQLSV.Views;

namespace WpfQLSV
{
    public partial class MainWindow : Window
    {
        private readonly StudentsView _studentsView;
        private readonly StudentsViewModel _studentsViewModel;
        private readonly SubjectsView _subjectsView; // Thêm SubjectsView
        private readonly SubjectsViewModel _subjectsViewModel; // Thêm SubjectsViewMode
        private readonly ClassesView _classesView; 
        private readonly ClassesViewModel _classesViewModel;
        private readonly ScoresView _scoresView;
        private readonly ScoresViewModel _scoresViewModel;

        public MainWindow(StudentsView studentsView, StudentsViewModel studentsViewModel, SubjectsView subjectsView,
            SubjectsViewModel subjectsViewModel , ClassesView classesView, ClassesViewModel classesViewModel, ScoresView scoresView, ScoresViewModel scoresViewModel)
        {
            InitializeComponent();

            // Lưu trữ StudentsView và StudentsViewModel được inject
            _studentsView = studentsView;
            _studentsViewModel = studentsViewModel;
            _subjectsView = subjectsView;
            _subjectsViewModel = subjectsViewModel;
            _classesView = classesView;
            _classesViewModel = classesViewModel;
            _scoresView = scoresView;
            _scoresViewModel = scoresViewModel;

            // Gán DataContext cho MainWindow
            DataContext = _studentsViewModel;
        }

        private void BtnStudents_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị StudentsView trong MainContent
            MainContent.Content = _studentsView;
            // Gán DataContext cho StudentsView
            _studentsView.DataContext = _studentsViewModel;
        }
        private void BtnSubjects_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị SubjectsView trong MainContent
            MainContent.Content = _subjectsView;

            // Gán DataContext cho SubjectsView
            _subjectsView.DataContext = _subjectsViewModel;
        }
        private void BtnClasses_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = _classesView;
            _classesView.DataContext = _classesViewModel;
        }
        private void BtnScores_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = _scoresView;
            _scoresView.DataContext = _scoresViewModel;
        }
    }
}