namespace Homework_1._4._1
{
    interface ICommand
    {
        void Action();
        void Abort();
        string GetName();
        bool IsAvailability();
    }
}
