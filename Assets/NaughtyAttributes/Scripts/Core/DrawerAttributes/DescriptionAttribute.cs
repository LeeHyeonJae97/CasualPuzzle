using System;

namespace NaughtyAttributes
{
	public enum DescriptionType
	{
		Info,
		Warning
	}

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class DescriptionAttribute : DrawerAttribute
	{
		public DescriptionType Type { get; private set; }
		public string Url { get; private set; }

		public DescriptionAttribute()
		{
			Type = DescriptionType.Info;
			Url = null;
		}

		public DescriptionAttribute(DescriptionType type)
		{
			Type = type;
			Url = null;
		}

		public DescriptionAttribute(string url)
		{
			Type = DescriptionType.Info;
			Url = url;
		}

		public DescriptionAttribute(DescriptionType type, string url)
		{
			Type = type;
			Url = url;
		}
	}
}
