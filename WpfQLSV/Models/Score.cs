using System;
using System.Collections.Generic;

namespace WpfQLSV.Models;

public partial class Score
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public double Dtp { get; set; }

    public double Dkt { get; set; }

    public double Dgk { get; set; }

    public double Dck { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
