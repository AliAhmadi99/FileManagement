using System;
using System.Collections.Generic;

namespace OopExercise.FileManagement.Domain.Models
{
    public class Folder : Node
    {
        public Folder(string name, string creator)
            : base(name, creator)
        {
        }

        public List<Node> Nodes { get; set; }

        public override int GetSize()
        {
            int totalSize = 0;
            foreach (var item in Nodes)
            {
                totalSize += item.GetSize();
            }
            return totalSize;
        }
    }
}
