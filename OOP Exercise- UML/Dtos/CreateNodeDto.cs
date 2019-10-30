using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OopExercise.FileManagement.Web.Dtos
{
    public class CreateNodeDto
    {
        public CreateNodeDto(string name, string creatorName)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreatorName = creatorName ?? throw new ArgumentNullException(nameof(creatorName));
        }

        public string Name { get; }
        public string CreatorName { get; }
    }
}
