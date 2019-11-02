using Microsoft.AspNetCore.Mvc;
using OopExercise.FileManagement.Domain.Models;
using OopExercise.FileManagement.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OopExercise.FileManagement.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FileManagementController : Controller
    {
        private static Folder CurrentDirectory = new Folder("Root", "Administrator", null);

        public FileManagementController()
        {

        }

        [HttpPost]
        public IActionResult CreateFolder(CreateNodeDto newNode)
        {
            var newFolder = new Folder(newNode.Name, newNode.CreatorName, CurrentDirectory);
            CurrentDirectory.Add(newFolder);
            return Ok(new FolderViewModel(CurrentDirectory));
        }

        [HttpPost]
        public IActionResult CreateFile(CreateNodeDto newNode)
        {
            var newFile = new File(newNode.Name, newNode.CreatorName, CurrentDirectory);
            CurrentDirectory.Add(newFile);
            return Ok(new FolderViewModel(CurrentDirectory));
        }

        [Route("{name}")]
        public IActionResult GoToDirectory(string name)
        {
            var targetDirectory = GetNode(name);
            if (targetDirectory is Folder folder) CurrentDirectory = folder;
            return Ok(new FolderViewModel(CurrentDirectory));
        }

        public IActionResult Back()
        {
            CurrentDirectory = CurrentDirectory.ParentFolder ?? throw new Exception("You are in root address!");
            return Ok(new FolderViewModel(CurrentDirectory));
        }

        [HttpPost]
        public IActionResult Rename(RenameNodeDto renameNodeDto)
        {
            var node = GetNode(renameNodeDto.OldName);
            node.Rename(renameNodeDto.NewName);
            return Ok(new FolderViewModel(CurrentDirectory));
        }

        [Route("{name}")]
        public IActionResult GetSize(string name)
        {
            var node = GetNode(name);
            return Ok(node.GetSize());
        }

        [HttpPost, Route("{name}")]
        public IActionResult Remove(string name)
        {
            var fileToRemove = GetNode(name);
            CurrentDirectory.Remove(fileToRemove);
            return Ok(new FolderViewModel(CurrentDirectory));
        }

        public IActionResult GetCurrentDirectory() => Ok(CurrentDirectory);

        #region Private Methods
        public Node GetNode(string nodeName)
        {
            var node = CurrentDirectory.Nodes.SingleOrDefault(f => f.Name
            .Equals(nodeName, StringComparison.OrdinalIgnoreCase)) ??
            throw new Exception(nodeName + " Not Found!");
            return node;
        }
        #endregion
    }
}

