using System.ComponentModel.DataAnnotations;

namespace TssT.API.Contracts.Test
{
    public class NewTest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }
    }
}