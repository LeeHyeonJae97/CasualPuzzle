using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class PropertySpaceAttribute : DrawerAttribute
	{
		public float Upper { get; private set; }
		public float Under { get; private set; }

		public PropertySpaceAttribute(float upper = 0, float under = 0)
		{
			Upper = upper;
			Under = under;
		}
	}
}
