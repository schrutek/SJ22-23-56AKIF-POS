using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Test.Helpers
{
    public class DateTimeServiceMock : IDateTimeService
    {
        public DateTime Now => new DateTime(2023, 02, 28);
    }
}
