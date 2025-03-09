using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfQLSV.Models;

namespace WpfQLSV.ViewModels
{
    public class ScoresViewModel : ObservableObject
    {
        private Subject _selectedSubject;
        private ObservableCollection<Score> _filteredScores;

        public ObservableCollection<Score> ScoreList { get; set; }
        public ObservableCollection<Subject> Subjects { get; set; }
        public ICommand SaveCommand { get; }

        public ObservableCollection<Score> FilteredScores
        {
            get => _filteredScores;
            set
            {
                _filteredScores = value;
                OnPropertyChanged();
            }
        }

        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                _selectedSubject = value;
                OnPropertyChanged();
                UpdateFilteredScores();
            }
        }

        public ScoresViewModel()
        {
            LoadData();
            SaveCommand = new RelayCommand(SaveScores);

        }
        private void SaveScores()
        {
            using (var db = new StudentMngContext())
            {
                foreach (var score in FilteredScores)
                {
                    if (score.Dtp < 0 || score.Dtp > 10 ||
                        score.Dgk < 0 || score.Dgk > 10 ||
                        score.Dck < 0 || score.Dck > 10)
                    {
                        MessageBox.Show("Điểm số phải nằm trong khoảng từ 0 đến 10!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var existingScore = db.Scores.FirstOrDefault(s => s.Id == score.Id);
                    if (existingScore != null)
                    {
                        existingScore.Dtp = score.Dtp;
                        existingScore.Dgk = score.Dgk;
                        existingScore.Dck = score.Dck;
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Lưu điểm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void LoadData()
        {
            using (var db = new StudentMngContext()) // Thay bằng DbContext của bạn
            {
                // Lấy danh sách môn học
                Subjects = new ObservableCollection<Subject>(db.Subjects.ToList());

                // Lấy toàn bộ điểm số, kèm theo thông tin sinh viên và môn học
                ScoreList = new ObservableCollection<Score>(
                    db.Scores.Select(s => new Score
                    {
                        Id = s.Id,
                        StudentId = s.StudentId,
                        Student = db.Students.FirstOrDefault(st => st.Id == s.StudentId),
                        SubjectId = s.SubjectId,
                        Subject = db.Subjects.FirstOrDefault(sub => sub.Id == s.SubjectId),
                        Dtp = s.Dtp,
                        Dkt = s.Dkt,
                        Dgk = s.Dgk,
                        Dck = s.Dck
                    }).ToList()
                );
            }

            _filteredScores = new ObservableCollection<Score>();
        }

        private void UpdateFilteredScores()
        {
            if (SelectedSubject != null)
            {
                FilteredScores = new ObservableCollection<Score>(
                    ScoreList.Where(s => s.SubjectId == SelectedSubject.Id).ToList()
                );
            }
            else
            {
                FilteredScores = new ObservableCollection<Score>(ScoreList);
            }
        }

    }
}
