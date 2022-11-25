using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Basics.Demo.Delegates
{
    public delegate bool CompareHandler(int a, int b);

    public class OldSchool
    {
        public bool GreatMethodForNearlyEverything(CompareHandler handler, int x, int y)
        {
            // ...
            // 1000 Zeilen Code (z.B. irgendweche Vorbereitungen werden duhgeführt)
            // ...

            bool result = handler(x, y);

            if (result == true)
            {
                // Do something
            }
            else
            {
                // Do something else
            }

            // ...
            // 1000 Zeilen Code (z.B. result wird irgendwie verarbeitet)
            // ...

            return result;
        }


        public bool CompareEqual(int x, int y)
        {
            return x == y;
        }

        public bool CompareGreater(int x, int y)
        {
            return x > y;
        }

        public void DoSomeWork()
        {
            Console.WriteLine(GreatMethodForNearlyEverything(CompareGreater, 5, 12));
        }
    }
}
