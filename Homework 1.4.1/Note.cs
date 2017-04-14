namespace Homework_1._4._1
{
    class Note
    {
        public string Name { get; private set; }
        public string Message { get; private set; }
        public User Owner { get; private set; }

        public Note(string name, string message, User owner)
        {
            Name = name;
            Message = message;
            Owner = owner;
        }
    }
}
