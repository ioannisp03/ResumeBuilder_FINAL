using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder_FINAL
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }

        public override string ToString()
        {
            string display = String.Format("{0}\t {1}\t {2}\t {3}\t {4}\t {5}\t {6}", Id, FirstName, LastName, Age, PhoneNumber, Email, Position);
            return display;
        }
    }
}
