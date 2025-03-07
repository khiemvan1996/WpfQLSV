using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WpfQLSV.Models;

namespace WpfQLSV.ViewModels
{
    public partial class SubjectsViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<SubjectViewModel> _subjects;

        public SubjectsViewModel()
        {
            LoadSubjects();
        }

        private void LoadSubjects()
        {
            using (var context = new StudentMngContext())
            {
                // Lấy danh sách môn học
                var subjects = context.Subjects.ToList();

                // Tạo Dictionary để đếm số lượng sinh viên theo môn học
                var studentCounts = context.StudentsSubjects
                    .GroupBy(ss => ss.SubjectId)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Tạo danh sách SubjectViewModel (chứa TotalStudents)
                Subjects = subjects.Select(subject => new SubjectViewModel
                {
                    Id = subject.Id,
                    SubjectName = subject.SubjectName,
                    Credit =subject.Credit,
                    TotalStudents = studentCounts.ContainsKey(subject.Id) ? studentCounts[subject.Id] : 0 // Kiểm tra tránh lỗi
                }).ToList();
            }
        }
    }

    // ✅ Thêm class SubjectViewModel để chứa dữ liệu hiển thị
    public class SubjectViewModel
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int Credit { get; set; }
        public int TotalStudents { get; set; }
    }
}
