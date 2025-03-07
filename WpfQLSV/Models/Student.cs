using System;
using System.Collections.Generic;

namespace WpfQLSV.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int? IdClass { get; set; }

    public DateTime DateOfBirth { get; set; }

    public virtual Class? IdClassNavigation { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<StudentsSubject> StudentsSubjects { get; set; } = new List<StudentsSubject>();
    public string SubjectsRegistered => string.Join(", ", Scores.Select(s => s.Subject.SubjectName));

}
