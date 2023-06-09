﻿using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.IO;

namespace HPackage.Net
{
    public partial class HollowKnightPackageDef
    {
        static readonly JSchema schema;

        static HollowKnightPackageDef()
        {
            using Stream s = typeof(HollowKnightPackageDef).Assembly.GetManifestResourceStream("HPackage.Net.Schema");
            using StreamReader sr = new(s);
            schema = JSchema.Parse(sr.ReadToEnd());
            Converter.Settings.ContractResolver = new PreferredOrderContractResolver();
            Converter.Settings.Converters.Add(new SortedStringMapWriterConverter<ReferenceVersion>());
        }

        /// <summary>
        /// Parses and validates a JSON string as a HollowKnightPackageDef.
        /// </summary>
        /// <param name="content">The JSON to parse.</param>
        /// <returns>A schema-compliant package definition.</returns>
        /// <exception cref="ValidationException">Thrown when validation errors are present.</exception>
        public static HollowKnightPackageDef FromJsonValidated(string content)
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

        /// <summary>
        /// Converts a HollowKnightPackageDef to a JSON string.
        /// </summary>
        /// <param name="formatting">The formatting style to use.</param>
        /// <returns>A schema-compliant JSON string.</returns>
        /// <exception cref="ValidationException">Thrown when validation errors are present.</exception>
        public string ToJsonValidated(Formatting formatting = Formatting.Indented)
        {
            StringWriter sw = new();
            JsonTextWriter writer = new(sw);
            writer.Formatting = formatting;
            writer.Indentation = 4;
            writer.IndentChar = ' ';
            JSchemaValidatingWriter validatingWriter = new(writer);
            validatingWriter.Schema = schema;

            List<string> errors = new();
            validatingWriter.ValidationEventHandler += (_, a) => errors.Add(a.Message);

            JsonSerializer serializer = JsonSerializer.Create(Converter.Settings);
            serializer.Serialize(validatingWriter, this);

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
            return sw.ToString();
        }
    }
}
