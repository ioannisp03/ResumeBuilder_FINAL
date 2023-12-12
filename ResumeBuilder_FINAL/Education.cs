using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ResumeBuilder_FINAL
{
    public class Education
    {
        public int Id { get; set; }
        public string AcademicDegree { get; set; }
        public string Major_FieldOfStudy { get;set; }
        public string InstitutionName{ get; set; }
        public int YearOfCompletion { get; set; }   
        public string Details { get; set; }




        public override string ToString()
        {
            string formatted = String.Format("{0}\t {1}\t {2}\t | {3}\t {4}\t {5}", Id, AcademicDegree, Major_FieldOfStudy, InstitutionName, YearOfCompletion,Details);
            return formatted;
        }

    }
}
