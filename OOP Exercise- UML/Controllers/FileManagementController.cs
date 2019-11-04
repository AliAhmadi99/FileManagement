using Microsoft.AspNetCore.Mvc;
using OopExercise.FileManagement.Domain.Models;
using OopExercise.FileManagement.Web.Dtos;
using OopExercise.FileManagement.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OopExercise.FileManagement.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FileManagementController : Controller
    {
        private readonly FileManager fileManager;
        public FileManagementController()
        {
            var root = new Folder("Root", "Administrator", null);
            fileManager = new FileManager(root);
        }

        [HttpPost]
        public IActionResult CreateFolder(CreateNodeDto newNode)
        {
            fileManager.Add(newNode.Name, newNode.CreatorName);
            return Ok(fileManager.GetCurrentDirectory());
        }

        [HttpPost]
        public IActionResult CreateFile(CreateNodeDto newNode)
        {
            fileManager.Add(newNode.Name, newNode.CreatorName);
            return Ok();
        }

        [Route("{name}")]
        public IActionResult GoToDirectory(string name)
        {
            fileManager.GoToDirectory(name);
            return Ok(fileManager.GetCurrentDirectory());
        }

        public IActionResult Back()
        {
            fileManager.Back();
            return Ok(fileManager.GetCurrentDirectory());
        }

        [HttpPost]
        public IActionResult Rename(RenameNodeDto renameNodeDto)
        {
            fileManager.Rename(renameNodeDto.OldName, renameNodeDto.NewName);
            return Ok(fileManager.GetCurrentDirectory());
        }

        [Route("{name}")]
        public IActionResult GetSize(string name)
        {
            var size = fileManager.GetSize(name);
            return Ok(size);
        }

        [HttpPost, Route("{name}")]
        public IActionResult Remove(string name)
        {
            fileManager.Remove(name);
            return Ok(fileManager.GetCurrentDirectory());
        }

        public IActionResult GetCurrentDirectory() => Ok(fileManager.GetCurrentDirectory());
    }
}

