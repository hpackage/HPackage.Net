# HPackage.Net

.NET client package for Hollow Knight PackageDefs. Types are genreated from the schema at https://github.com/hpackage/hpackage-schema
using [quicktype](https://github.com/quicktype/quicktype). Quicktype does not generate documentation on the generated classes, you can
reference the documentation in the schema itself for information on the schema types.

Consumers are strongly recommended to use the `ToJsonValidated` and `FromJsonValidated` methods in `HollowKnightPackageDef`. Because of how
unions are handled by quicktype, in many cases, unions are flattened down to a single class. For example, the `Reference` class is a merger
of the 3 different types of references. Without using `ToJsonValidated`, it is possible to generate invalid objects in your code. Usage of
`FromJsonValidated` is less important, but can ensure that your input JSON is valid as well. Both methods throw `ValidationException` if
the object structure is not compliant with the schema. The normal `FromJson` and `ToJson` methods can perform the conversion without validation,
if that's desired for some reason.
