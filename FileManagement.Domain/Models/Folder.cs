using System;
using System.Collections.Generic;
using System.Linq;

namespace OopExercise.FileManagement.Domain.Models
{
    public class Folder : Node
    {
        public Folder(string name, string creator, Folder parent)
            : base(name, creator, parent)
        {
        }

        private List<Node> _nodes = new List<Node>();
        public IReadOnlyCollection<Node> Nodes => _nodes.AsReadOnly();

        public override int GetSize()
        {
            int totalSize = 0;
            foreach (var item in Nodes)
            {
                totalSize += item.GetSize();
            }
            return totalSize;
        }
        public void Add(Node node)
        {
            var nodeName = node.Name;
            ValidateName(nodeName);
            if (_nodes.Any(node => node.Name.Equals(nodeName, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("A file or folder with this name is already exist.");
            _nodes.Add(node);
        }
        public void Remove(Node node)
        {
            _nodes.Remove(node ?? throw new ArgumentNullException(nameof(node)));
        }
    }
}
