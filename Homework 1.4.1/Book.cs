using System.Collections.Generic;

namespace Homework_1._4._1
{
    class Book
    {
        public string Name { get; private set; }
        public List<Note> Notes { get; private set; }

        public Book(string name)
        {
            Name = name;
        }
    }
}
