using System.ComponentModel.DataAnnotations;

namespace TssT.Core.Contracts.Test
{
    public class TestCreateRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }
    }
}