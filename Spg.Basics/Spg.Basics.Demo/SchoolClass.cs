using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Basics.Demo
{
    public class SchoolClass
    {
        public string Name 
        {
            get 
            {
                return _name;
            }
            set
            {
                if (value.Length <= 1)
                {
                    //throw new ArgumentException("Klassenname ist zu kurz!");
                }
                _name = value;
            }
        }
        private string _name = string.Empty;

        //public SchoolClass(string name, int maxStudents)
        //{
        //    _name = name;
        //    MaxStudents = maxStudents;
        //}

        public int MaxStudents { get; init; }

        //public void SetMaxStudents(int maxStudents)
        //{
        //    MaxStudents = maxStudents;
        //}

        public List<Student> Students { get; set; } = new();
    }
}
