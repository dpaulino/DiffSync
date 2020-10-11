using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return objOld.Patch(diffResults.Differences);
        }

        public static T Patch<T>(this T objOld, IList<Difference> diffResults)
        {
            if (diffResults == null || diffResults.Count == 0)
            {
                return objOld;
            }

            JObject x = JObject.FromObject(objOld);

            foreach (var diff in diffResults)
            {
                if (diff == null)
                {
                    continue;
                }

                JToken jprop = x.SelectToken(diff.ParentPropertyName);
                string propertyName = diff.PropertyName.Remove(0, diff.ParentPropertyName.Length).TrimStart('.');
                jprop[propertyName] = JToken.FromObject(diff.Object2);
            }

            return x.ToObject<T>();
        }

        public static T Merge<T>(
            this T commonObjBase,
            T obj1,
            T obj2,
            Func<Difference, Difference, Difference> ResolveCollision = null)
        {
            ComparisonResult objDiff = obj1.Diff(obj2);

            if (objDiff.AreEqual || commonObjBase == null)
            {
                return obj2;
            }

            ComparisonResult obj1ValidChanges = commonObjBase.Diff(obj1);
            ComparisonResult obj2ValidChanges = commonObjBase.Diff(obj2);

            var combinedValidChanges = new List<Difference>();

            foreach (var diff in objDiff.Differences)
            {
                Difference obj1Change = obj1ValidChanges.Differences.FirstOrDefault(x => x.PropertyName == diff.PropertyName);
                Difference obj2Change = obj2ValidChanges.Differences.FirstOrDefault(x => x.PropertyName == diff.PropertyName);

                if (obj1Change != null && obj2Change != null)
                {
                    Difference resolvedDiff = ResolveCollision == null ? diff : ResolveCollision(obj1Change, obj2Change);
                    combinedValidChanges.Add(resolvedDiff);
                }
                else if (obj1Change != null)
                {
                    combinedValidChanges.Add(obj1Change);
                }
                else
                {
                    combinedValidChanges.Add(diff);
                }
            }

            return commonObjBase.Patch(combinedValidChanges);
        }
    }
}
