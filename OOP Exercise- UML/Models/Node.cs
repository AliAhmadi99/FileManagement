using System;
namespace OOP_Exercise__UML.Models
{
    public abstract class Node
    {
        protected Node(string name, DateTime creationDate, string creator)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreationDate = creationDate;
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Creator { get; set; }
        protected void Rename(string newName)
        {
            Name = newName;
        }
        protected abstract void Remove();

    }
}
