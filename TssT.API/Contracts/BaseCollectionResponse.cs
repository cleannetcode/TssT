using System.Collections.Generic;

namespace TssT.API.Contracts
{
    public class BaseCollectionResponse<T>
    {
        public IReadOnlyCollection<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}