
using OopExercise.FileManagement.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace OopExercise.FileManagement.Web.Dtos
{
    public class FolderViewModel
    {
        public FolderViewModel(Node node)
        {
            Name = node.Name;
            Size = node.GetSize();
            if (node is Folder folder)
                SubNodes = folder.Nodes.Select(node => new FolderViewModel(node)).ToList();
        }
        public string Name { get; set; }
        public int Size { get; set; }
        public List<FolderViewModel> SubNodes { get; set; }
    }
}
