using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TssT.Api.Contracts
{
    public class NewTest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }
        
        public List<string> Topics { get; set; }
    }
}