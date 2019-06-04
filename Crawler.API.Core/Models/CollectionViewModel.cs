using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.API.Core.Models
{
    public class CollectionViewModel<T>
    {
        public CollectionViewModel()
        {
            Elements = new List<T>();
        }
        public long Total { get; set; }
        public long Count { get { return Elements.Count; } }
        public List<T> Elements { get; set; }
    }
}
