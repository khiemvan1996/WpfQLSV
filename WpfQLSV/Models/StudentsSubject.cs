using System;
using System.Collections.Generic;

namespace WpfQLSV.Models;

public partial class StudentsSubject
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
