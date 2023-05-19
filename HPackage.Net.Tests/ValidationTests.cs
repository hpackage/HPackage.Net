

using Newtonsoft.Json;

namespace HPackage.Net.Tests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("null")]
        // on this data point, note that assets is incorrectly specified (from an older schema revision, actually)
        // instead of the correct releaseAssets property
        [InlineData("""
            {
                "description": "Randomizer 4 addon that adds the option to randomize all other Hunter's Journal entries.",
                "authors": ["BadMagic"],
                "repository": "https://github.com/BadMagic100/TheRealJournalRando",
                "dependencies": [
                    "ItemChanger"
                ],
                "devDependencies": {
                    "Randomizer 4": "@modlinks",
                    "ItemSync": "@modlinks",
                    "RandoSettingsManager": "@latest",
                    "MoreLocations": "@latest"
                },
                "assets": [
                    "bin/Publish/TheRealJournalRando.zip"
                ]
            }
            """)]
        [InlineData("""
            {
                "additionalAssets": [
                    {
                        "installRootDir": "saves"
                    }
                ]
            }
            """)]
        public void InvalidObjectsThrowsValidationException(string content)
        {
            Action action = () => HollowKnightPackageValidation.ParseValidate(content);
            action.Should().Throw<ValidationException>().Which.Errors.Should().NotBeEmpty();
        }

        [Theory]
        // this could probably use some additional exercise but idk how to make bad json
        [InlineData("{")]
        public void InvalidJsonThrowsSerializationException(string content)
        {
            Action action = () => HollowKnightPackageValidation.ParseValidate(content);
            action.Should().Throw<JsonSerializationException>();
        }

        [Theory]
        [ClassData(typeof(ValidPackageDefs))]
        public void ValidObjectReturnsPackageObject(string content, HollowKnightPackageDef expectedPackageDef)
        {
            HollowKnightPackageDef actualDef = HollowKnightPackageValidation.ParseValidate(content);
            actualDef.Should().BeEquivalentTo(expectedPackageDef, options => options
                .ComparingByMembers<ReleaseAssets>()
                .ComparingByMembers<ReferenceVersion>()
                .ComparingByMembers<References>());
        }
    }
}