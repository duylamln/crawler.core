using System.Collections.Generic;

namespace Crawler.API.Core.Models.Firebase
{
    public class FirebaseCollection<T> : Dictionary<string, T>
    {
        public List<T> ToList()
        {
            var result = new List<T>();
            foreach (var value in Values)
            {
                result.Add(value);
            }
            return result;
        }
    }
}
