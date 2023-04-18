using Spg.Di.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Di.Application
{
    public class LoggingService : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
