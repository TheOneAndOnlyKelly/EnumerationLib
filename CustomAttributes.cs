using System;
using System.Collections.Generic;

namespace EnumerationLib
{
	[AttributeUsage(AttributeTargets.All)]
	public class DeprecatedAttribute : System.Attribute
	{
		public bool IsDeprecated = false;

		public DeprecatedAttribute()
		{
			this.IsDeprecated = true;
		}

		public DeprecatedAttribute(bool value)
		{
			this.IsDeprecated = value;
		}
	}

	[AttributeUsage(AttributeTargets.All)]
	public class StringValueAttribute : System.Attribute
	{
		public string Value = string.Empty;
		public StringValueAttribute(string value)
		{
			this.Value = value;
		}
	}

	[AttributeUsage(AttributeTargets.All)]
	public class UsedInInterviewQuestionsAttribute : System.Attribute
	{
		public bool Used = false;

		public UsedInInterviewQuestionsAttribute()
		{
			this.Used = true;
		}

		public UsedInInterviewQuestionsAttribute(bool value)
		{
			this.Used = value;
		}
	}
}
