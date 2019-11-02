using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OopExercise.FileManagement.Domain.Models
{
    public class File : Node
    {
        public File(string name, string creator, Folder parent)
            : base(name, creator, parent)
        {
            Format = GetFormat(name);
            Size = new Random().Next(100, 250000);
        }

        public string Format { get; private set; }
        public int Size { get; set; }

        public override int GetSize() => Size;
        private string GetFormat(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
            var format = fileName.Substring(fileName.LastIndexOf('.') + 1)?.ToLowerInvariant();
            ValidateName(format);
            return format;
        }

    }
}
