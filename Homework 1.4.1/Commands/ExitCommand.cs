using System;

namespace Homework_1._4._1
{
    class ExitCommand : Command
    {
        public ExitCommand(CommandController controller) : base(controller)
        {
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override void Action()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "ExitCommand";
        }

        public override bool IsAvailability()
        {
            return true;
        }

        public partial class SomeFileModule { }
    }
}
