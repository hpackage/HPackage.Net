using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPackage.Net.Tests.Data
{
    internal class SerializeInvalidPackages : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // multiple subschemas of oneof
            yield return Single(new HollowKnightPackageDef()
            {
                Dependencies = new Dictionary<string, ReferenceVersion>()
                {
                    ["Randomizer 4"] = new ReferenceDef()
                    {
                        Ref = new Reference()
                        {
                            UseLatestPublished = true,
                            UseLatestRelease = true,
                        }
                    }
                }
            });
            // no schemas of oneof
            yield return Single(new HollowKnightPackageDef()
            {
                Dependencies = new Dictionary<string, ReferenceVersion>()
                {
                    ["Randomizer 4"] = new ReferenceDef()
                    {
                        Ref = new Reference()
                    }
                }
            });
            // missing required props, no oneof involved
            yield return Single(new HollowKnightPackageDef()
            {
                AdditionalAssets = new()
                {
                    new AdditionalAsset()
                    {
                        InstallPath = "CustomKnight/Skins/MyFancyCustomSkin",
                        InstallRootDir = InstallationRoot.Mods
                    }
                }
            });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private object[] Single(HollowKnightPackageDef def)
        {
            return new object[] { def };
        }
    }
}
