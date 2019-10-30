using Microsoft.AspNetCore.Mvc;
using OopExercise.FileManagement.Domain.Models;
using OopExercise.FileManagement.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OopExercise.FileManagement.Web.Controllers
{
    [ApiController]
    //[Route("[controller]/{action}")]
    [Route("api/Salam")]
    public class FileManagementController : Controller
    {
        public FileManagementController()
        {

        }
        private static List<Node> CurrentDirectory = new List<Node>();

        [HttpPost]
        public IActionResult CreateFolder(CreateNodeDto newNode)
        {
            var newFolder = new Folder(newNode.Name, newNode.CreatorName);
            CurrentDirectory.Add(newFolder);
            return Ok();
        }

        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("salam test !");
        }

        [HttpPost]
        public IActionResult CreateFile(CreateNodeDto newNode)
        {
            var newFile = new File(newNode.Name, newNode.CreatorName);
            CurrentDirectory.Add(newFile);
            return Ok();
        }

        [Route("{name:alpha}")]
        public IActionResult GoToDirectory(string name)
        {
            var currentDir = GetNode(name);
            if (currentDir is Folder folder) CurrentDirectory = folder.Nodes;
            return Ok(CurrentDirectory);
        }

        public IActionResult Back()
        {
            var firstNodeInCurrentDirectory = CurrentDirectory.FirstOrDefault();
            CurrentDirectory = firstNodeInCurrentDirectory.GetParentDirectory();
            return Ok(CurrentDirectory);
        }

        [HttpPost]
        public IActionResult Rename(string oldName, string newName)
        {
            var node = GetNode(oldName);
            node.Rename(newName);
            return Ok();
        }

        [Route("{name:alpha}")]
        public IActionResult GetSize(string name)
        {
            var node = GetNode(name);
            return Ok(node.GetSize());
        }

        [HttpPost]
        public IActionResult Remove(string name)
        {
            var fileToRemove = GetNode(name);
            CurrentDirectory.Remove(fileToRemove);
            return Ok();
        }

        public IActionResult GetCurrentDirectory() => Ok(CurrentDirectory);

        #region Private Methods
        private Node GetNode(string name) =>
            CurrentDirectory.SingleOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        #endregion
    }
}

