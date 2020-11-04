using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManagement.Domain.Models
{
    public abstract class Node
    {
        protected Node(string name, string creator, Folder parentFolder)
        {
            ParentFolder = parentFolder;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreationDate = DateTime.Now;
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
        }

        public string Name { get; protected set; }
        public DateTime CreationDate { get; }
        public string Creator { get; }
        public Folder ParentFolder { get; private set; }


        public void Rename(string newName)
        {
            ValidateName(newName);
            Name = newName;
        }
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
        public void MoveToAnotherFolder(Folder targetFolder)
        {
            ParentFolder = targetFolder ??
                throw new ArgumentNullException(nameof(targetFolder));
        }

        public string GetPath()
        {
            var parent = ParentFolder;
            var path = Name;
            while (parent != null)
            {
                path = $@"{parent.Name}\{path}";
                parent = parent.ParentFolder;
            }
            return path;
        }
    }
}
