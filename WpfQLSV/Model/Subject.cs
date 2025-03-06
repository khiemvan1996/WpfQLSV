using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace WpfQLSV.Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public List<Score> Scores { get; set; } = new();
    }
}
