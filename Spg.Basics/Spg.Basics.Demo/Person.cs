using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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

    public class Person : IParsable<Person>
    {
        private string _lastName = "";

        public Person()
        { }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

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

        public static Person Parse(string s, IFormatProvider? provider)
        {
            string[] strings = s.Split(new[] { ',', ';' });
            if (strings.Length != 2) 
            { 
                throw new OverflowException("Expect: FirstName,LastName"); 
            }
            return new Person(strings[0], strings[1]);
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Person result)
        {
            result = null;
            if (s == null) 
            { 
                return false; 
            }
            try
            {
                result = Parse(s, provider);
                return true;
            }
            catch 
            { 
                return false; 
            }
        }
    }

    public static class ExtensionMethods
    { 
        public static T Parse<T>(this string s) 
            where T : IParsable<T>
        {
            return T.Parse(s, null);
        }
    }
}
