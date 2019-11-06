using OopExercise.FileManagement.Web.Dtos;
using OopExercise.FileManagement.FileSystemManager;
using System;
using Xunit;

namespace OopExercise.FileManagement.Test
{
    public class FileManagementScenario
    {
        private readonly FileManager fileManager = new FileManager("Root", "Administrator");
        private CreateNodeDto _createRequest;

        [Fact]
        void CreateFile_Success()
        {
            _createRequest = new CreateNodeDto
            {
                Name = "test Video.mp4",
                CreatorName = "Ali Ahmadi"
            };
            fileManager.AddFile(_createRequest.Name, _createRequest.CreatorName);
            var fileInfo = fileManager.GetNodeInfo(_createRequest.Name);
            Assert.NotNull(fileInfo);
            Assert.Equal(_createRequest.Name, fileInfo.Name);
            Assert.Equal(_createRequest.CreatorName, fileInfo.Creator);
            Assert.True(fileInfo.Size >= 100 && fileInfo.Size < 250000);
            Assert.Equal(fileInfo.CreationDate, DateTime.Now.ToString("d"));
            var fileFormat = _createRequest.Name.Substring(_createRequest.Name.LastIndexOf('.') + 1).ToLowerInvariant();
            Assert.Equal(fileFormat, fileInfo.Format);
            Assert.Empty(fileInfo.SubNodes);
        }

        [Fact]
        void CreateFolder_Success()
        {
            _createRequest = new CreateNodeDto
            {
                Name = "New Folder",
                CreatorName = "John Doe"
            };
            fileManager.AddFolder(_createRequest.Name, _createRequest.CreatorName);
            var folder = fileManager.GetNodeInfo(_createRequest.Name);
            Assert.NotNull(folder);
            Assert.Equal(_createRequest.Name, folder.Name);
            Assert.Equal(_createRequest.CreatorName, folder.Creator);
            Assert.Equal(0, folder.Size);
            Assert.Equal(folder.CreationDate, DateTime.Now.ToString("d"));
            Assert.Null(folder.Format);
            Assert.Empty(folder.SubNodes);
        }

        [Fact]
        void RenameFolder_Success()
        {
            CreateFolder_Success();
            var renameRequest = new RenameNodeDto
            {
                OldName = _createRequest.Name,
                NewName = "Music Folder"
            };
            fileManager.Rename(renameRequest.OldName, renameRequest.NewName);
            var folder = fileManager.GetNodeInfo(renameRequest.NewName);
            Assert.NotNull(folder);
            Assert.Equal(renameRequest.NewName, folder.Name);
        }

        [Fact]
        void RenameFile_Success()
        {
            CreateFile_Success();
            var renameRequest = new RenameNodeDto
            {
                OldName = _createRequest.Name,
                NewName = "My Movie.mp4"
            };
            fileManager.Rename(renameRequest.OldName, renameRequest.NewName);
            var file = fileManager.GetNodeInfo(renameRequest.NewName);
            Assert.NotNull(file);
            Assert.Equal(renameRequest.NewName, file.Name);
        }

        [Fact]
        void OpenNode_Success()
        {
            CreateFolder_Success();
            fileManager.Open(_createRequest.Name);
            var currentDirectory = fileManager.GetCurrentDirectory();
            Assert.Equal(_createRequest.Name, currentDirectory.Name);
            Assert.Equal(_createRequest.CreatorName, currentDirectory.Creator);
        }

        [Fact]
        void CheckNodeSize_Success()
        {
            _createRequest = new CreateNodeDto
            {
                Name = "New Folder3",
                CreatorName = "James"
            };
            var totalSize = 0;
            fileManager.AddFolder(_createRequest.Name, _createRequest.CreatorName);
            fileManager.Open(_createRequest.Name);
            for (int i = 0; i < 10; i++)
            {
                _createRequest.Name = $"file{i}.mp4";
                fileManager.AddFile(_createRequest.Name, _createRequest.CreatorName);
                totalSize += fileManager.GetNodeInfo(_createRequest.Name).Size;
            }
            _createRequest.Name = "Folder28";
            fileManager.AddFolder(_createRequest.Name, _createRequest.Name);
            fileManager.Open(_createRequest.Name);
            _createRequest.Name = "MyFile.pdf";
            fileManager.AddFile(_createRequest.Name, _createRequest.CreatorName);
            totalSize += fileManager.GetNodeInfo(_createRequest.Name).Size;
            fileManager.Back();
            fileManager.Back();
            var currentDirectory = fileManager.GetCurrentDirectory();
            Assert.Equal(totalSize, currentDirectory.Size);
        }
    }
}
