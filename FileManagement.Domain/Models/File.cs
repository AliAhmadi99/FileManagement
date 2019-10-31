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

        public string Format { get; set; }
        public int Size { get; set; }
        public override int GetSize() => Size;

        public override void Rename(string newName)
        {
            var splitedName = newName.Split('.');
            ValidateName(splitedName[0]);
            Format = GetFormat(splitedName[1]);
            Name = newName;
        }

        private string GetFormat(string format)
        {
            if (string.IsNullOrWhiteSpace(format)) throw new ArgumentNullException(nameof(format));
            ValidateName(format);
            return format;
        }

    }
}
