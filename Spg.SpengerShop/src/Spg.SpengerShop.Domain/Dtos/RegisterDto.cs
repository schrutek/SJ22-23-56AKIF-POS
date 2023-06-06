using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class RegisterDto
    {
        public Genders Gender { get; set; }

        [StringLength(8, ErrorMessage = "Username darf nicht mehr alös 8 zeichen lang sein!!!", MinimumLength = 5)]
        [Required(ErrorMessage = "Username ist NOTWENDIG!!!")]
        public string UserName { get; set; } = string.Empty;

        [StringLength(120, MinimumLength = 1)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(120, MinimumLength = 1)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress()]
        public string EMail { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;

        //[RegularExpression("^[a-zA-Z0-9]")]
        [StringLength(20, MinimumLength = 4)]
        [Compare(nameof(Password), ErrorMessage = "PWDs sind NICHT gleich!")]
        public string ConfirmPassword { get; set; } = string.Empty;

        //[Range(typeof(DateTime), "2007.05.23", "2011.05.23")]
        public DateTime BirthDate { get; set; }
   }
}
