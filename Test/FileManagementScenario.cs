using Microsoft.AspNetCore.Mvc;
using OopExercise.FileManagement.Domain.Models;
using OopExercise.FileManagement.Web.Controllers;
using OopExercise.FileManagement.Web.Dtos;
using OopExercise.FileManagement.Web.Services;
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
        private readonly FileManager fileManager = new FileManager("Root", "Administrator");

        [Fact]
        void CreateFile_Success()
        {
            var request = new CreateNodeDto
            {
                Name = "test Video.mp4",
                CreatorName = "Ali Ahmadi"
            };
            fileManager.AddFile(request.Name, request.CreatorName);
            var fileInfo = fileManager.GetNodeInfo(request.Name);
            Assert.NotNull(fileInfo);
            Assert.Equal(request.Name, fileInfo.Name);
            Assert.Equal(request.CreatorName, fileInfo.Creator);
            Assert.True(fileInfo.Size >= 100 && fileInfo.Size < 250000);
            Assert.Equal(fileInfo.CreationDate, DateTime.Now.ToString("d"));
            var fileFormat = request.Name.Substring(request.Name.LastIndexOf('.') + 1)?.ToLowerInvariant();
            Assert.Equal(fileFormat, fileInfo.Format);
            Assert.Empty(fileInfo.SubNodes);
        }

        [Fact]
        void CreateFolder_Success()
        {
            var request = new CreateNodeDto
            {
                Name = "New Folder",
                CreatorName = "John Doe"
            };
            fileManager.AddFolder(request.Name, request.CreatorName);
            var folder = fileManager.GetNodeInfo(request.Name);
            Assert.NotNull(folder);
            Assert.Equal(request.Name, folder.Name);
            Assert.Equal(request.CreatorName, folder.Creator);
            Assert.Equal(0, folder.Size);
            Assert.Equal(folder.CreationDate, DateTime.Now.ToString("d"));
            Assert.Null(folder.Format);
            Assert.Empty(folder.SubNodes);
        }

        [Fact]
        void 
    }
}
