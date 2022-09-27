using System.ComponentModel.DataAnnotations;

namespace Spg.Basics.Demo
{
    public enum Genders
    {
        Male,
        Female,
        Other
    }

    public partial class Student
    {
        private string _lastName = "";


        [Key]
        public int Id { get; set; }
        public Genders Gender { get; set; } // 0=Male, 1=Female, 2=Other
        public string Name { get; set; }

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
