using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json.Linq;
using System;

namespace DiffSync
{
    public static class Extensions
    {
        public static ComparisonResult Diff<T>(this T objOld, T objNew)
        {
            CompareLogic compareLogic = new CompareLogic();
            compareLogic.Config.MaxDifferences = int.MaxValue;
            return compareLogic.Compare(objOld, objNew);
        }

        public static T Patch<T>(this T objOld, ComparisonResult diffResults)
        {
            JObject x = JObject.FromObject(objOld);

            foreach (var diff in diffResults.Differences)
            {
                x[diff.PropertyName] = JToken.FromObject(diff.Object2);
            }

            return x.ToObject<T>();
        }

        public static T Merge<T>(this T obj1, T obj2)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
