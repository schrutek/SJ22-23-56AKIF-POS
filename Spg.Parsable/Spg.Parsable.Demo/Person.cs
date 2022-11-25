using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Parsable.Demo
{
    public class Person : IParsable<Person>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; private set; }

        /// <summary>
        /// Leerkonstructor immer gut, weil EF-Core diesen benötigt.
        /// Bitte vor unerlaubtem Zugriff verstecken (<code>protected</code>)
        /// </summary>
        protected Person()
        { }
        public Person(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        /// <summary>
        /// Simple implementierung des Interfaces. <code>string</code> zerlegen 
        /// und den Konstrukor verwenden um eine befüllte Instanz zu erstellen.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        public static Person Parse(string s, IFormatProvider? provider)
        {
            if (s is null)
            {
                throw new ArgumentNullException("Input war NULL!");
            }
            string[] result = s.Split(new char[] { ',', ';', '\t'});
            if (result.Length != 3)
            {
                throw new ArgumentException("Input muss bestehen aus: FirstName,LastName,Birthdate");
            }
            DateTime birthDate;
            if (!DateTime.TryParse(result[2], out birthDate))
            {
                throw new FormatException("Geburtsdatum hat falsches Format!");
            }
            return new Person(result[0].Trim(), result[1].Trim(), birthDate);
        }

        /// <summary>
        /// Hier ist wichtig! Hier erfolgt eine Prüfung auf NULL, obwohl, die Parse-Methode aufgerufen wird,
        /// in der ebenfalls diese Prüfung erfolgt. (Doppelt gemoppelt)
        /// Das ist aber OK (sogar wichtig), weil so 1x eine möglicher try-catch-Block vermieden
        /// wird, der auf die Laufzeit drücken würde. 2 If sind performanter als 1 try-catch
        /// </summary> 
        /// <param name="s"></param>
        /// <param name="provider"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse([NotNullWhen(true)] string? s, 
            IFormatProvider? provider, 
            [MaybeNullWhen(false)] out Person result)
        {
            result = null;
            if (s is null)
            {
                return false;
            }
            try
            {
                result = Parse(s, provider);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
