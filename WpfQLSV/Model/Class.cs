using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQLSV.Model
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public List<Student> Students { get; set; } = new();
    }
}
