using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Basics.Demo.Delegates
{
    public delegate bool FilterHandler(Student s);

    public class MyStudentList : List<Student>
    {
        public MyStudentList Filter(FilterHandler predicate)
        {
            MyStudentList list = new MyStudentList();
            foreach (Student item in this)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}
