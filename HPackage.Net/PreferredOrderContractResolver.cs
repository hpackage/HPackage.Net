using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HPackage.Net
{
    internal class PreferredOrderContractResolver : DefaultContractResolver
    {
        private static readonly string[] PropertyNames = new string[]
        {
            "name",
            "description",
            "authors",
            "repository",
            "dependencies",
            "devDependencies",
            "releaseAssets",
            "additionalAssets",
        };
        private static readonly Dictionary<string, int> PropertyOrders = PropertyNames
            .Select((s, i) => (s, i - 1 - PropertyNames.Length))
            .ToDictionary(x => x.s, x => x.Item2);

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            if (type == typeof(HollowKnightPackageDef))
            {
                return props.OrderBy(p =>
                {
                    int order = p.Order ?? 0;
                    if (p.PropertyName != null && PropertyOrders.TryGetValue(p.PropertyName, out int i))
                    {
                        order = i;
                    }
                    p.Order = order;
                    return order;
                }).ThenBy(p => p.PropertyName).ToList();
            }
            else
            {
                return props;
            }
        }
    }
}
