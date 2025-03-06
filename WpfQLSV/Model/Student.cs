using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace WpfQLSV.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Class")]
        public int IdClass { get; set; }
        public Class Class { get; set; }
        public List<Score> Scores { get; set; } = new();
    }
}
