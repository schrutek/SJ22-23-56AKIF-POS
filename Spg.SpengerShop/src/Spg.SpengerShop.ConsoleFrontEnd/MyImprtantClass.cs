using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.ConsoleFrontEnd
{
    public class MyImprtantClass
    {
        private string message = string.Empty;

        public void Method1()
        {
            message = "Hello World";
        }

        public void Method2()
        {
            Console.WriteLine(message);
        }
    }
}
