using System;
using System.Linq;

namespace Sinkbox
{
	public static class EnumTools
	{
		public static T[] All<T>() where T : Enum
			=> typeof(T).GetEnumValues()
						.Cast<T>()
						//.Select(o => (T) o)
						.ToArray();

		[AttributeUsage(AttributeTargets.Field)]
		public class EnumStrAttribute : Attribute
		{
			public string str;
			public EnumStrAttribute(string str) => this.str = str;
		}

		public static string GetEnumStr<T>(T item) where T : Enum
		{
			var type = item.GetType();
			if (!type.IsEnum) throw new ArgumentException("item must be of Enum type", nameof(item));

			//Tries to find a DescriptionAttribute for a potential friendly name
			//for the enum
			var memberInfo = type.GetMember(item.ToString()!);
			if (memberInfo.Length > 0)
			{
				var attrs = memberInfo[0].GetCustomAttributes(typeof(EnumStrAttribute), false);

				if (attrs.Length > 0)
					//Pull out the description value
					return ((EnumStrAttribute) attrs[0]).str;
			}

			//If we have no attribute, just return the ToString of the enum
			return item.ToString();
		}

		public static T? GetByStr<T>(string itemName) where T : Enum
			=> All<T>().FirstOrDefault(enumValue => GetEnumStr(enumValue) == itemName);
	}
}