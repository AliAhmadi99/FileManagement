﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OopExercise.FileManagement.Domain.Models
{
    public class File : Node
    {
        public File(string name, string creator)
            : base(name, creator)
        {
            Format = GetFormat(name);
            Size = new Random().Next(100, 250000);
        }

        public string Format { get; set; }
        public int Size { get; set; }
        public override int GetSize() => Size;

        private string GetFormat(string fileName)
        {
            var format = fileName.Split('.')[1];
            ValidateName(format);
            return format;
        }

    }
}