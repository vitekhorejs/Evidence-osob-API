using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_osob_API
{
    public class Person
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string BIRTHNUMBER1 { get; set; }
        public string BIRTHNUMBER2 { get; set; }
        public DateTime BIRTHDATE { get; set; }
        public string GENDER { get; set; }
        //public string Added { get; set; }
        //public string Edited { get; set; }
        public int Age
        {
            get { return _age(); }
        }
        public int _age()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - BIRTHDATE.Year;
            if (now < BIRTHDATE.AddYears(age)) age--;
            return age;
        }

        public override string ToString()
        {
            return SURNAME + " " + NAME + "  " + BIRTHNUMBER1+"/"+ BIRTHNUMBER2;
        }
    }
}
