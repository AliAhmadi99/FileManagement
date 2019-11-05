using OopExercise.FileManagement.Domain.Models;
using OopExercise.FileManagement.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OopExercise.FileManagement.Web.Services
{
    public class FileManager
    {
        private static Folder currentDirectory;

        public FileManager(string name, string creator)
        {
            currentDirectory = new Folder(name, creator, null);
        }

        public void AddFolder(string name, string creator)
        {
            var newNode = new Folder(name, creator, currentDirectory);
            currentDirectory.Add(newNode);
        }
        public void AddFile(string name, string creator)
        {
            var newNode = new File(name, creator, currentDirectory);
            currentDirectory.Add(newNode);
        }

        private Node GetNode(string nodeName)
        {
            var node = currentDirectory.Nodes.SingleOrDefault(f => f.Name
            .Equals(nodeName, StringComparison.OrdinalIgnoreCase)) ??
            throw new Exception(nodeName + " Not Found!");
            return node;
        }

        public void GoToDirectory(string name)
        {
            var targetDirectory = GetNode(name);
            if (targetDirectory is Folder folder) currentDirectory = folder;
            else Debug.WriteLine($"File {name} opened!");
        }

        public void Back()
        {
            currentDirectory = currentDirectory.ParentFolder ??
                throw new Exception("You are in root address!");
        }

        public void Rename(string oldName, string newName)
        {
            var node = GetNode(oldName);
            node.Rename(newName);
        }

        public int GetSize(string name)
        {
            var node = GetNode(name);
            return node.GetSize();
        }

        public void Remove(string name)
        {
            var fileToRemove = GetNode(name);
            currentDirectory.Remove(fileToRemove);
        }

        public FolderViewModel GetCurrentDirectory()
        {
            return new FolderViewModel(currentDirectory);
        }

        public FolderViewModel GetNodeInfo(string nodeName)
        {
            var node = GetNode(nodeName);
            return new FolderViewModel(node);
        }
    }
}
