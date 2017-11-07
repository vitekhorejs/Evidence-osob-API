using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_osob_API
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthNumber1 { get; set; }
        public string BirthNumber2 { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        //public string Added { get; set; }
        //public string Edited { get; set; }

        public int Age
        {
            get { return _age(); }
        }
        public int _age()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - BirthDate.Year;
            if (now < BirthDate.AddYears(age)) age--;
            return age;
        }

        public override string ToString()
        {
            return Surname + " " + Name + "  " + BirthNumber1+"/"+ BirthNumber2 + " vek: "+Age + " datum: "+ BirthDate.ToShortDateString();
        }
    }
}
