using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HPackage.Net
{
    public static class HollowKnightPackageValidation
    {
        static readonly JSchema schema;

        static HollowKnightPackageValidation()
        {
            using Stream s = typeof(HollowKnightPackageValidation).Assembly.GetManifestResourceStream("HPackage.Net.Schema");
            using StreamReader sr = new(s);
            schema = JSchema.Parse(sr.ReadToEnd());
        }

        public static HollowKnightPackageDef ParseValidate(string content)
        {
            JsonTextReader reader = new(new StringReader(content));
            JSchemaValidatingReader validatingReader = new(reader) { Schema = schema };

            List<string> errors = new();
            validatingReader.ValidationEventHandler += (_, a) => errors.Add(a.Message);

            JsonSerializer serializer = JsonSerializer.Create(Converter.Settings);
            HollowKnightPackageDef? def = serializer.Deserialize<HollowKnightPackageDef>(validatingReader);

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
            // if the root value is null, that should actually fail validation so we can be confident that def is nonnull here
            return def!;
        }
    }
}
