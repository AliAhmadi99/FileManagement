using System;
using System.Collections.Generic;

namespace OopExercise.FileManagement.Domain.Models
{
    public abstract class Node
    {
        protected Node(string name, string creator)
        {
            ValidateName(name);
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreationDate = DateTime.Now;
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Creator { get; set; }
        public Folder ParentFolder { get; set; }
        public void Rename(string newName)
        {
            ValidateName(newName);
            Name = newName;
        }

        public List<Node> GetParentDirectory()
        {
            return ParentFolder.ParentFolder.Nodes;
        }
        public abstract int GetSize();
        protected void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            var folderNameOrFileNameWithoutFormat = name.Split('.')[0];
            var unAllowedCharachters = new List<char>()
            {
                '\\',
                '/',
                ':',
                '*',
                '?',
                '"',
                '<',
                '>',
                '|',
                '.',
            };
            for (int i = 0; i < unAllowedCharachters.Count; i++)
            {
                if (folderNameOrFileNameWithoutFormat.Contains(unAllowedCharachters[i]))
                    throw new Exception($"You can't use '{unAllowedCharachters[i]}' in your file name.");
            }
        }
    }
}
