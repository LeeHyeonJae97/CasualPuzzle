using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class FoldoutAttribute : MetaAttribute, IGroupAttribute
	{
		public string Name { get; private set; }
		public bool IsFieldName { get; private set; }
		public int InnerPriority { get; private set; }			

		public FoldoutAttribute(string name, bool isFieldName = false, int innerPriority = 0)
		{
			Name = name;
			IsFieldName = isFieldName;
			InnerPriority = innerPriority;
		}
	}
}
