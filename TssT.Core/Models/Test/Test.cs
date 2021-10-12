using System.Collections.Generic;

namespace TssT.Core.Models.Test
{
    public class Test
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public IReadOnlyCollection<Topic> Topics { get; set; }
    }
}