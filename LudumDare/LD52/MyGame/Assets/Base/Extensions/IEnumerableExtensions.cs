using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Libs.Base.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T GetRandom<T>(this IEnumerable<T> data)
        {
            var index = Random.Range(0, data.Count());
            return data.ElementAt(index);
        }
    }
}