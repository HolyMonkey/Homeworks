using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1._4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Database mainDataBase = new Database();
            CommandController Controller = new CommandController(mainDataBase);

            while (true)
            {
                string commandName = Console.ReadLine();
                Command command = Controller.ResolveCommand(commandName);
                if(command != null && command.IsAvailability())
                {
                    command.Action();
                }
                
            }
        }
    }
}
