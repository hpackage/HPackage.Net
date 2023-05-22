using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPackage.Net.Tests.Data
{
    internal class ValidPackageDefJsonPairs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return Pair("{}", new HollowKnightPackageDef());
            yield return Pair("""
                {
                    "name": "TheRealJournalRando",
                    "description": "Randomizer 4 addon that adds the option to randomize all other Hunter's Journal entries.",
                    "authors": [
                        "BadMagic"
                    ],
                    "repository": "https://github.com/BadMagic100/TheRealJournalRando",
                    "dependencies": [
                        "ItemChanger"
                    ],
                    "devDependencies": {
                        "ItemSync": "@modlinks",
                        "MoreLocations": "@latest",
                        "Randomizer 4": {
                            "alternateInstallName": "RandomizerMod",
                            "ref": {
                                "useLatestPublished": true
                            }
                        },
                        "RandoSettingsManager": "@latest"
                    },
                    "releaseAssets": "bin/Publish/TheRealJournalRando.zip"
                }
                """,
                new HollowKnightPackageDef()
                {
                    Name = "TheRealJournalRando",
                    Description = "Randomizer 4 addon that adds the option to randomize all other Hunter's Journal entries.",
                    Authors = new List<string>() { "BadMagic" },
                    Repository = new Uri("https://github.com/BadMagic100/TheRealJournalRando"),
                    Dependencies = new List<string>()
                    {
                        "ItemChanger"
                    },
                    DevDependencies = new Dictionary<string, ReferenceVersion>()
                    {
                        ["Randomizer 4"] = new ReferenceDef()
                        {
                            Ref = new Reference() { UseLatestPublished = true },
                            AlternateInstallName = "RandomizerMod"
                        },
                        ["ItemSync"] = "@modlinks",
                        ["RandoSettingsManager"] = "@latest",
                        ["MoreLocations"] = "@latest"
                    },
                    ReleaseAssets = "bin/Publish/TheRealJournalRando.zip"
                });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private object[] Pair(string content, HollowKnightPackageDef def)
        {
            return new object[] { content, def };
        }
    }
}
