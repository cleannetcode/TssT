using System.ComponentModel.DataAnnotations;

namespace TssT.DataAccess.Entities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}