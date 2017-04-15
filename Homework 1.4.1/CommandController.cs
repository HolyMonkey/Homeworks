using System;
using System.Collections.Generic;

namespace Homework_1._4._1
{
    class CommandController
    {
        protected List<Command> commands;
        public Database db;

        public CommandController(Database db)
        {
            this.db = db;

            commands = new List<Command>
            {
                new ExitCommand(this),
                new ShowAllUsersCommand(this),
                new AddUserCommand(this)
            };
        }

        public Command ResolveCommand(string name)
        {
            return commands.Find((x) => x.GetName() == name);
        }
    }
}
