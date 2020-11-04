using FileManagement.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileManagement.FileSystemManager.ViewModels
{
    public class NodeInfoViewModel
    {
        public NodeInfoViewModel(Node node)
        {
            Name = node.Name;
            Size = node.GetSize();
            SubNodes = node is Folder folder ?
                folder.Nodes.Select(node => new NodeInfoViewModel(node)).ToList() :
                new List<NodeInfoViewModel>();
            if (node is File file) Format = file.Format;
            Creator = node.Creator;
            CreationDate = node.CreationDate.ToString("d");
            ParentFolderName = node.ParentFolder?.Name;
        }

        public string Name { get; }
        public int Size { get; }
        public List<NodeInfoViewModel> SubNodes { get; }
        public string Format { get; set; }
        public string Creator { get; set; }
        public string CreationDate { get; }
        public string ParentFolderName { get; }
    }
}
