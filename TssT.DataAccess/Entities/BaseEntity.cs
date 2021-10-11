using System.ComponentModel.DataAnnotations;

namespace TssT.DataAccess.Entities
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}