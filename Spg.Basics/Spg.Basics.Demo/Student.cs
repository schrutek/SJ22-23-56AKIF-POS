using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Spg.Basics.Demo
{
    public partial class Student : Person
    {
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"{FirstName} - {LastName} - {BirthDate}";
        }
    }
}
