using FileManagement.FileSystemManager;
using System;
using Xunit;
using FileManagement.FileSystemManager.ViewModels;

namespace FileManagement.Test
{
    public class FileManagementScenario
    {
        private readonly FileManager fileManager = new FileManager("Root", "Administrator");

        [Fact]
        void CreateFile_Success()
        {
            var createdFile = CreateFile("Test Video.mp4", "Ali Ahmadi");
            Assert.NotNull(createdFile);
            var fileInfo = fileManager.GetNodeInfo(createdFile.Name);
            Assert.NotNull(fileInfo);
            Assert.Equal(createdFile.Name, fileInfo.Name);
            Assert.Equal(createdFile.Creator, fileInfo.Creator);
            Assert.True(fileInfo.Size >= 100 && fileInfo.Size < 250000);
            Assert.Equal(fileInfo.CreationDate, DateTime.Now.ToString("d"));
            Assert.NotEmpty(fileInfo.Format);
            var fileFormat = createdFile.Name.Substring(createdFile.Name.LastIndexOf('.') + 1).ToLowerInvariant();
            Assert.Equal(fileFormat, fileInfo.Format);
        }

        [Fact]
        void CreateFolder_Success()
        {
            string name = "Videos", creator = "Ali Ahmadi";
            var folder = CreateFolder(name, creator);
            Assert.NotNull(folder);
            Assert.Equal(name, folder.Name);
            Assert.Equal(creator, folder.Creator);
            Assert.Equal(0, folder.Size);
            Assert.Equal(folder.CreationDate, DateTime.Now.ToString("d"));
            Assert.Null(folder.Format);
            Assert.Empty(folder.SubNodes);
        }

        [Fact]
        void RenameFolder_Success()
        {
            var createdFolder = CreateFolder();
            var renameRequest = new RenameNodeDto
            {
                OldName = createdFolder.Name,
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
            var createdFile = CreateFolder();
            var renameRequest = new RenameNodeDto
            {
                OldName = createdFile.Name,
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
            var createdFolder = CreateFolder();
            fileManager.Open(createdFolder.Name);
            var currentDirectory = fileManager.GetCurrentDirectory();
            Assert.Equal(createdFolder.Name, currentDirectory.Name);
            Assert.Equal(createdFolder.Creator, currentDirectory.Creator);
        }

        [Fact]
        void CheckNodeSize_Success()
        {
            var createdNode = CreateFolder("New Folder3", "James");
            var totalSize = 0;
            fileManager.Open(createdNode.Name);
            for (int i = 0; i < 10; i++)
            {
                var newMp4File = CreateFile($"file{i}.mp4", createdNode.Creator);
                totalSize += fileManager.GetNodeInfo(newMp4File.Name).Size;
            }
            var folder28 = CreateFolder("Folder28", createdNode.Creator);
            fileManager.Open(folder28.Name);
            var myPdf = CreateFile("MyFile.pdf", createdNode.Creator);
            totalSize += fileManager.GetNodeInfo(myPdf.Name).Size;
            fileManager.Back();
            fileManager.Back();
            var currentDirectory = fileManager.GetCurrentDirectory();
            Assert.Equal(totalSize, currentDirectory.Size);
        }

        #region Private Methods
        private NodeInfoViewModel CreateFolder(string name = "New Folder", string owner = "John Doe")
        {
            fileManager.AddFolder(name, owner);
            var folder = fileManager.GetNodeInfo(name);
            return folder;
        }

        private NodeInfoViewModel CreateFile(string name, string owner = "John Doe")
        {
            fileManager.AddFile(name, owner);
            var folder = fileManager.GetNodeInfo(name);
            return folder;
        }
        #endregion
    }
}
