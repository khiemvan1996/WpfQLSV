using System;
using System.Collections.Generic;

namespace WpfQLSV.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string SubjectName { get; set; } = null!;
    public int Credit { get; set; }
    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<StudentsSubject> StudentsSubjects { get; set; } = new List<StudentsSubject>();
         
  

}
