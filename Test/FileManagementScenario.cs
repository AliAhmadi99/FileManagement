using Microsoft.AspNetCore.Mvc;
using OopExercise.FileManagement.Domain.Models;
using OopExercise.FileManagement.Web.Controllers;
using OopExercise.FileManagement.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OopExercise.FileManagement.Test
{
    public class FileManagementScenario
    {
        private readonly FileManagementController fileManager = new FileManagementController();
        private static Folder CurrentDirectory = new Folder("Root", "Administrator", null);

        [Fact]
        void CheckCurrentDirectory_Success()
        {
            var result = fileManager.GetCurrentDirectory() as ObjectResult;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            var folderInfo = result.Value as Folder;
            Assert.Equal(CurrentDirectory.Name, folderInfo.Name);
            Assert.Equal(CurrentDirectory.Creator, folderInfo.Creator);
            Assert.Equal(CurrentDirectory.Nodes, folderInfo.Nodes);
            Assert.Equal(folderInfo.ParentFolder, CurrentDirectory.ParentFolder);
        }

        [Fact]
        void CreateFile_Success()
        {
            CheckCurrentDirectory_Success();
            var request = new CreateNodeDto
            {
                Name = "test Video.mp4",
                CreatorName = "Ali Ahmadi"
            };
            var result = fileManager.CreateFile(request) as ObjectResult;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            //var fileInfo = fileManager.GetNode(request.Name) as File;
            //Assert.Equal(request.Name, fileInfo.Name);
            //Assert.Equal(request.CreatorName, fileInfo.Creator);
            //Assert.True(fileInfo.Size >= 100 && fileInfo.Size < 250000);
            //Assert.Equal(fileInfo.CreationDate.Date, DateTime.Now.Date);
            var fileFormat = request.Name.Substring(request.Name.LastIndexOf('.') + 1)?.ToLowerInvariant();
            Assert.Equal(fileFormat, fileInfo.Format);
            CurrentDirectory = fileInfo.ParentFolder;
            CheckCurrentDirectory_Success();
        }
    }
}
