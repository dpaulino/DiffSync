using JsonDiffPatchDotNet;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace DiffSync
{
    public static class Extensions
    {
        /// <summary>
        /// Returns null if objects are identical.
        /// </summary>
        public static JToken Diff<T>(this T objOld, T objNew)
        {
            if (objOld == null) throw new ArgumentNullException(nameof(objOld));
            if (objNew == null) throw new ArgumentNullException(nameof(objNew));

            var jdp = new JsonDiffPatch();
            var left = JToken.FromObject(objOld);
            var right = JToken.FromObject(objNew);
            return jdp.Diff(left, right);
        }

        public static T Patch<T>(this T objOld, JToken patch)
        {
            var jdp = new JsonDiffPatch();
            JToken left = JToken.FromObject(objOld);
            var result = jdp.Patch(left, patch);
            return result.ToObject<T>();
        }

        /// <summary>
        /// Merges the forks onto the common base. Latter
        /// forks will overwrite any conflicts from former
        /// forks.
        /// </summary>
        /// <param name="commonObjBase">The most recent ancestor of the forks.</param>
        /// <param name="forks">
        /// The different forks that will be merged. 
        /// Merge conflicts are resolved by taking the later fork in the list.
        /// </param>
        /// <returns>A merged object.</returns>
        public static T Merge<T>(this T commonObjBase, params T[] forks)
        {
            T rollingResult = commonObjBase;
            foreach (JToken diff in forks.Select(x => commonObjBase.Diff(x)))
            {
                rollingResult = rollingResult.Patch(diff);
            }
            return rollingResult;
        }
    }
}
