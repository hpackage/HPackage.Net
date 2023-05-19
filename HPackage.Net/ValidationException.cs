using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HPackage.Net
{


	[Serializable]
	public class ValidationException : Exception
	{
		public readonly IReadOnlyList<string> Errors;

		public ValidationException(IList<string> errors) : base("One or more validation errors occurred.")
		{
			Errors = new ReadOnlyCollection<string>(errors);
		}

		public ValidationException(IList<string> errors, Exception inner) : base("One or more validation errors occurred.", inner)
		{
			Errors = new ReadOnlyCollection<string>(errors);
		}
	}
}
