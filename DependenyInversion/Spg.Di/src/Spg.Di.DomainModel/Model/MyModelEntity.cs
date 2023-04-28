using Spg.Di.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Di.DomainModel.Model
{
    public class MyModelEntity
    {
        private readonly ILogger _logger;

        public MyModelEntity(ILogger logger)
        {
            _logger = logger;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public void DoSomething()
        {
            _logger.Log("Log that something was done!");
        }
    }
}
