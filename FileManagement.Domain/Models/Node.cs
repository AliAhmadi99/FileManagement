using System;
using System.Collections.Generic;
using System.Linq;

namespace OopExercise.FileManagement.Domain.Models
{
    public abstract class Node
    {
        protected Node(string name, string creator, Folder parentFolder)
        {
            ParentFolder = parentFolder;
            ValidateName(name);
            if (ParentFolder?.Nodes.Any(node => node.Name.Equals(name)) is true)
                throw new Exception("A file or folder with this name already exist.");
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreationDate = DateTime.Now;
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Creator { get; set; }
        public Folder ParentFolder { get; set; }
        public abstract void Rename(string newName);
        public abstract int GetSize();
        protected void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            var unAllowedCharachters = new List<string>()
            {
                "\\",
                "/",
                ":",
                "*",
                "?",
                "\"",
                "<",
                ">",
                "|",
            };
            for (int i = 0; i < unAllowedCharachters.Count; i++)
            {
                if (name.Contains(unAllowedCharachters[i]))
                    throw new Exception($"You can't use '{unAllowedCharachters[i]}' in your file name.");
            }
        }
    }
}
