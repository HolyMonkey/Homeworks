using System;

namespace Homework_1._4._1
{
    class AddUserCommand : Command
    {
        public AddUserCommand(CommandController controller) : base(controller)
        {
        }

        public override void Abort()
        {
            return;
        }

        public override void Action()
        {
            Console.WriteLine("Введите имя пользователя:");

            string newUserName = Console.ReadLine();
            controller.db.Users.Add(new User(newUserName));

            Console.WriteLine("Пользователь успешно добавлен!");
        }

        public override string GetName()
        {
            return "AddUser";
        }

        public override bool IsAvailability()
        {
            return true;
        }
    }
}
