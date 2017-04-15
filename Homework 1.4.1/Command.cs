namespace Homework_1._4._1
{
    abstract class Command
    {
        protected CommandController controller;

        public abstract void Action();
        public abstract void Abort();
        public abstract string GetName();
        public abstract bool IsAvailability();

        public Command(CommandController controller)
        {
            this.controller = controller;
        }
    }
}
