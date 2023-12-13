using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder_FINAL
{
    public class Experience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string StartedDate { get; set; }
        public string EndedDate { get; set; }
        public string Position { get; set; }

        public override string ToString()
        {
            string display = String.Format("{0}\t {1}\t {2}\t {3}\t {4}", Id, CompanyName, StartedDate, EndedDate, Position);
            return display;
        }
    }
}
