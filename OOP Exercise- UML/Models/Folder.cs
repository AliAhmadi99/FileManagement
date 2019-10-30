using System;
using System.Collections.Generic;

namespace OOP_Exercise__UML.Models
{
    public class Folder : Node
    {
        public Folder(string name, DateTime creationDate, string creator)
            : base(name, creationDate, creator)
        {
        }

        public IList<Node> Nodes { get; set; }

        protected override void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
