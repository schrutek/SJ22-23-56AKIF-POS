using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Parsable.Demo
{
    /// <summary>
    /// Extension-Methodes (= syntethical Sugar) sind statische Methoden
    /// die einen bestimmten Datentypen um eine Funktion erweitern. Der Datentyp
    /// ist als Parameter angegeben, ist immer der erste Parameter und wird mit dem Keyword 
    /// <code>this</code> versehen.
    /// Hier ist der zu erweiterndse Datentyp <code>string</code>. <code>string</code> erhält 
    /// also dadurch eine weitere Methode, die es vorher nicht gab.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Beispiel non-generic
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int CountWords(this string s)
        {
            return s.Split(' ').Length;
        }

        /// <summary>
        /// Generic!
        /// Hier muss geprüft werden <code>where</code>, ob der Datentyp
        /// die geforderte Methode enthält - sprich - das Interface wirklich 
        /// implementiert. Dann kann die Methode sicher aufgerufen werden.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T Parse<T>(this string s) 
            where T : IParsable<T>
        {
            return T.Parse(s, null);
        }
    }
}
