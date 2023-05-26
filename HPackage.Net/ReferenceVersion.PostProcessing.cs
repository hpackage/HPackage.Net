using System;
using System.Text.RegularExpressions;

namespace HPackage.Net
{
    public partial struct ReferenceVersion
    {
        private static readonly Regex modlinksVersionRegex = new(@"^(\d+\.){3}\d+$");

        /// <summary>
        /// Fetches or constructs the final ReferenceDef representing this reference, processing the string version if needed.
        /// The created ReferenceDef may not be valid for serialization as modlinks versions and Git tag names are ambiguous.
        /// </summary>
        /// <returns>The ReferenceDef represented by this ReferenceVersion.</returns>
        /// <exception cref="InvalidOperationException">Thrown when both ReferenceDef and String versions are null.</exception>
        public ReferenceDef GetReferenceDef()
        {
            if (this.ReferenceDef != null)
            {
                return this.ReferenceDef;
            }
            if (this.String == null)
            {
                throw new InvalidOperationException("ReferenceDef and String are both null; one is needed to construct the correct ReferenceDef.");
            }
            string version = this.String;
            if (Uri.TryCreate(version, UriKind.Absolute, out Uri uriVersion))
            {
                return new ReferenceDef
                {
                    Ref = new Reference
                    {
                        Link = uriVersion,
                    }
                };
            }
            else if (version == "@modlinks")
            {
                return new ReferenceDef
                {
                    Ref = new Reference
                    {
                        UseLatestPublished = true,
                    }
                };
            }
            else if (version == "@latest")
            {
                return new ReferenceDef
                {
                    Ref = new Reference
                    {
                        UseLatestRelease = true,
                    }
                };
            }
            else if (modlinksVersionRegex.IsMatch(version))
            {
                return new ReferenceDef
                {
                    Ref = new Reference
                    {
                        Version = version,
                    }
                };
            }
            else
            {
                return new ReferenceDef
                {
                    Ref = new Reference
                    {
                        Tag = version,
                    }
                };
            }
        }
    }
}
