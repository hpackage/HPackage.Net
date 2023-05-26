namespace HPackage.Net.Tests
{
    public class VersionProcessingTests
    {
        [Fact]
        public void EmptyReferenceVersionThrows()
        {
            Action action = () => new ReferenceVersion().GetReferenceDef();
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ReferenceVersionWithReferenceDefReturnsReferenceDef()
        {
            ReferenceVersion version = new ReferenceDef
            {
                AlternateInstallName = "A",
                FileType = FileType.Dll,
                Ref = new Reference
                {
                    UseLatestPublished = true,
                }
            };
            version.GetReferenceDef().Should().BeSameAs(version.ReferenceDef);
        }

        [Fact]
        public void UriVersionReturnsLinkDef()
        {
            ReferenceVersion version = "https://example.com/someFile.dll";
            ReferenceDef expected = new ReferenceDef
            {
                Ref = new Reference
                {
                    Link = new Uri("https://example.com/someFile.dll")
                }
            };
            version.GetReferenceDef().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ModlinksReturnsModlinksReference()
        {
            ReferenceVersion version = "@modlinks";
            ReferenceDef expected = new ReferenceDef
            {
                Ref = new Reference
                {
                    UseLatestPublished = true,
                }
            };
            version.GetReferenceDef().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ModlinksVersionFormatReturnsModlinksReference()
        {
            ReferenceVersion version = "1.0.1.0";
            ReferenceDef expected = new ReferenceDef
            {
                Ref = new Reference
                {
                    Version = "1.0.1.0"
                }
            };
            version.GetReferenceDef().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void LatestReturnsGitReference()
        {
            ReferenceVersion version = "@latest";
            ReferenceDef expected = new ReferenceDef
            {
                Ref = new Reference
                {
                    UseLatestRelease = true,
                }
            };
            version.GetReferenceDef().Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("v1.0.1.0")]
        [InlineData("adasdfasdf")]
        public void AnythingElseReturnsGitReference(string versionString)
        {
            ReferenceVersion version = versionString;
            ReferenceDef expected = new ReferenceDef
            {
                Ref = new Reference
                {
                    Tag = versionString
                }
            };
            version.GetReferenceDef().Should().BeEquivalentTo(expected);
        }
    }
}
