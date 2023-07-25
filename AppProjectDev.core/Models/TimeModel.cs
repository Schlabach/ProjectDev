using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectDev.core.Models
{
    public class TimeModel
    {
        public int ID { get; set; }
        public string IDString { get { return ID.ToString(); } }
        public string Name { get; set; }
        public int Project_ID { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Start_Date_String
        {
            get
            {
                if (Start_Date.Day != DateTime.Now.Day)
                {
                    return Start_Date.ToString("MM/dd hh:mmtt");
                }
                else
                {
                    return Start_Date.ToString("hh:mmtt");
                }
            }
        }
        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}
