using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Exercise__UML.Models
{
    public class File : Node
    {
        public File(string format, double size, Folder folder, string name, DateTime creationDate, string creator)
            : base(name, creationDate, creator)
        {
            Format = format ?? throw new ArgumentNullException(nameof(format));
            Size = size;
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
            var d = new File("", 1d, new Folder("", DateTime.Now, ""), "", DateTime.Now, "");
        }

        public string Format { get; set; }
        public double Size { get; set; }
        public Folder Folder { get; set; }

        protected override void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
