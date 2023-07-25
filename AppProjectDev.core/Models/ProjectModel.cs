using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectDev.core.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public bool Is_Complete { get; set; }
        public float Planning { get; set; }
        public float Development { get; set; }
        public float Implementation { get; set; }
        public float Support { get; set; }
        public TimeSpan Time { get; set; }
        public string Time_String { get { return Time.ToString(@"hh\:mm"); } }
        public override string ToString()
        {
            return Name;
        }
    }
}