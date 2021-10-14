using System;
using System.ComponentModel.DataAnnotations;

namespace TssT.Core.Models.Test
{
    public class NewTest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}