using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace EnumerationLib
{
	public class EnumHelper
	{

		/// <summary>
		/// Returns an enumeration from the Description attribute, or the fieldname
		/// </summary>
		/// <typeparam name="T">Type to be returned</typeparam>
		/// <param name="description">Text to search for</param>
		/// <returns>Returns the Enum with this description or text, or the default value if not found</returns>
		public static T GetValueFromDescription<T>(string description)
		{
			var type = typeof(T);

			if (!type.IsEnum)
				throw new InvalidOperationException();

			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field,
					typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attribute != null)
				{
					if (attribute.Description == description)
						return (T)field.GetValue(null);
				}
				else
				{
					if (field.Name == description)
						return (T)field.GetValue(null);
				}
			}
			return default(T);

		}

		/// <summary>
		/// Returns an enumeration from the StringValue attribute, or the fieldname
		/// </summary>
		/// <typeparam name="T">Type to be returned</typeparam>
		/// <param name="value">Text to search for</param>
		/// <returns>Returns the Enum with this string value or text, or the default value if not found</returns>
		public static T GetValueFromStringValue<T>(string value)
		{
			var type = typeof(T);

			if (!type.IsEnum)
				throw new InvalidOperationException();

			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field,
					typeof(StringValueAttribute)) as StringValueAttribute;
				if (attribute != null)
				{
					if (attribute.Value == value)
						return (T)field.GetValue(null);
				}
				else
				{
					if (field.Name == value)
						return (T)field.GetValue(null);
				}
			}
			return default(T);

		}

		/// <summary>
		/// Finds the enumeration based on the numeric value
		/// </summary>
		/// <typeparam name="T">Type to be returned</typeparam>
		/// <param name="value">Numeric value</param>
		/// <returns></returns>
		public static T GetEnumFromValue<T>(int value)
		{
			try
			{
				return (T)System.Enum.ToObject(typeof(T), value);
			}
			catch
			{
				return default(T);
			}
		}

		/// <summary>
		/// Returns either the Description attribute from the enumeration, or the string equivalent it
		/// </summary>
		public static string GetEnumDescription(System.Enum value)
		{
			FieldInfo fi;
			DescriptionAttribute[] attributes;
			try
			{
				fi = value.GetType().GetField(value.ToString());

				attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attributes != null &&
					attributes.Length > 0)
					return attributes[0].Description;
				else
					return value.ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns either the StringValue attribute from the enumeration, or the string equivalent it
		/// </summary>
		public static string GetEnumStringValue(System.Enum value)
		{
			FieldInfo fi;
			StringValueAttribute[] attributes;
			try
			{
				fi = value.GetType().GetField(value.ToString());

				attributes = (StringValueAttribute[])fi.GetCustomAttributes(typeof(StringValueAttribute), false);

				if (attributes != null &&
					attributes.Length > 0)
					return attributes[0].Value;
				else
					return value.ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return string.Empty;
			}
		}

		/// <summary>
		/// If the enum has the Deprecated attribute set on it, then returns that value.
		/// </summary>
		public static bool IsDeprecated(System.Enum value)
		{
			FieldInfo fi;
			DeprecatedAttribute[] attributes;
			try
			{
				fi = value.GetType().GetField(value.ToString());

				attributes = (DeprecatedAttribute[])fi.GetCustomAttributes(typeof(DeprecatedAttribute), false);

				if (attributes != null &&
					attributes.Length > 0)
					return attributes[0].IsDeprecated;
				else
					return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return false;
			}
		}

		/// <summary>
		/// If the enum has been flagged to be for Interview Questions, then returns that value, else false
		/// </summary>
		public static bool IsUsedForInterviewQuestions(System.Enum value)
		{
			FieldInfo fi;
			UsedInInterviewQuestionsAttribute[] attributes;
			try
			{
				fi = value.GetType().GetField(value.ToString());

				attributes = (UsedInInterviewQuestionsAttribute[])fi.GetCustomAttributes(typeof(UsedInInterviewQuestionsAttribute), false);

				if (attributes != null &&
					attributes.Length > 0)
					return attributes[0].Used;
				else
					return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return false;
			}
		}

		/// <summary>
		/// Returns an enumerable list of value from an enum
		/// http://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static IEnumerable<T> GetValues<T>()
		{
			return System.Enum.GetValues(typeof(T)).Cast<T>();
		}
		
	}

	public static class EnumExtensions
	{
		// This extension method is broken out so you can use a similar pattern with 
		// other MetaData elements in the future. This is your base method for each.
		public static T GetAttribute<T>(this Enum value) where T : Attribute
		{
			var type = value.GetType();
			var memberInfo = type.GetMember(value.ToString());
			var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
			return (T)attributes[0];
		}
	}
}
