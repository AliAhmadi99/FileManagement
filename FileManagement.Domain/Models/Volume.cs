using System.Collections.Generic;

namespace FileManagement.Domain.Models
{
    public class Volume : Node
    {
        public Volume(string name, string creator, Folder parentFolder)
            : base(name, creator, parentFolder)
        {
        }

        public List<Node> Nodes { get; set; }

        public override int GetSize()
        {
            throw new System.NotImplementedException();
        }
    }
}
