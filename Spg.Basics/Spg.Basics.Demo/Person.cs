using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Basics.Demo
{
    public enum Genders
    {
        Male,
        Female,
        Other
    }

    public class Person
    {
        private string _lastName = "";

        [Key]
        public int Id { get; set; }
        public Genders Gender { get; set; } // 0=Male, 1=Female, 2=Other
        public string FirstName { get; set; } = string.Empty;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value != "Schrutek")
                {
                    _lastName = value;
                }
                else
                {
                    throw new NoSchruteksException("");
                }
            }
        }
    }
}
