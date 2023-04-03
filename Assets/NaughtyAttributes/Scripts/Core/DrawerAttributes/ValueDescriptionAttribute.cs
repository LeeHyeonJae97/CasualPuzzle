using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class ValueDescriptionAttribute : DrawerAttribute
	{
		public string Text { get; private set; }
		public DescriptionType Type { get; private set; }
		public string Url { get; private set; }

		public ValueDescriptionAttribute(string text, DescriptionType type)
		{
			Text = text;
			Type = type;
			Url = null;
		}

		public ValueDescriptionAttribute(string text, DescriptionType type, string url)
		{
			Text = text;
			Type = type;
			Url = url;
		}
	}
}
