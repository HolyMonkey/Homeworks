using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miscellaneous
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Что вы хотите запустить?");
            string program = Console.ReadLine();
            if(program == "RPGv1")
            {
                RPGv1.FakeMain();
            }
        }
    }
}
