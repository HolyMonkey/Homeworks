using System;

namespace Homework_1._4._1
{
    class ShowAllUsersCommand : Command
    {
        public ShowAllUsersCommand(CommandController controller) : base(controller)
        {
        }

        public override void Abort()
        {
            return;
        }

        public override void Action()
        {
            controller.db.Users.ForEach((x) => Console.WriteLine(x.Name));
        }

        public override string GetName()
        {
            return "ShowAllUsers";
        }

        public override bool IsAvailability()
        {
            return true;
        }
    }
}
