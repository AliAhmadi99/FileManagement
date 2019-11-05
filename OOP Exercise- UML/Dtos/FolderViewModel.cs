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
            SubNodes = node is Folder folder ?
                folder.Nodes.Select(node => new FolderViewModel(node)).ToList() :
                new List<FolderViewModel>();
            if (node is File file)Format = file.Format;
            Creator = node.Creator;
            CreationDate = node.CreationDate.ToString("d");
            ParentFolderName = node.ParentFolder.Name;
        }

        public string Name { get; }
        public int Size { get; }
        public List<FolderViewModel> SubNodes { get; }
        public string Format { get; set; }
        public string Creator { get; set; }
        public string CreationDate { get; }
        public string ParentFolderName { get; }
    }
}
