using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class HighRoller : Customer
    {
        public int BonusPoints { get; set; }
        public decimal MinTurnOver { get; set; }

        protected HighRoller() { }
        public HighRoller(
            Guid guid,
            Genders gender,
            long customerNumber,
            string firstName,
            string lastName,
            string eMail,
            DateTime birthDate,
            DateTime registrationDateTime,
            Address? address,
            int bonusPoints,
            decimal minTurnOver)
            :base(guid, gender, customerNumber, firstName, lastName, eMail, birthDate, registrationDateTime, address)
        {
            BonusPoints= bonusPoints;
            MinTurnOver = minTurnOver;
        }
    }
}
