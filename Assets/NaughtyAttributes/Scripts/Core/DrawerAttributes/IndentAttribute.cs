using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class IndentAttribute : DrawerAttribute
	{
		public int Level { get; private set; }

		public IndentAttribute(int level)
		{
			Level = UnityEngine.Mathf.Max(0, level);
		}
	}
}
