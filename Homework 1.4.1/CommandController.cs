using System;
using System.Collections.Generic;

namespace Homework_1._4._1
{
    class CommandController
    {
        private List<ICommand> commands;

        public CommandController()
        {
            commands = new List<ICommand>
            {
                new ExitCommand(),
                new ShowAllUsersCommand()
            };
        }

        public ICommand ResolveCommand(string name)
        {
            throw new NotImplementedException();
        }
    }
}
