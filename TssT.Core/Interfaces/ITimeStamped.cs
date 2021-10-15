using System;

namespace TssT.Core.Interfaces
{
    public interface ITimeStamped
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}