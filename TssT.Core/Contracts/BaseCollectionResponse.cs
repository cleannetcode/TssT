using System.Collections.Generic;

namespace TssT.Core.Contracts
{
    public class BaseCollectionResponse<T>
    {
        public IList<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}