using System;
using System.Xml.Linq;

namespace FEFTwiddler.Extensions
{
    public static class XElementExtensions
    {
        /// <summary>
        /// Get the value of the attribute with the given name
        /// </summary>
        public static string GetAttribute(this XElement xe, string attributeName, string defaultValue = null)
        {
            var attr = xe.Attribute(attributeName);
            if (attr == null) return defaultValue;
            var value = attr.Value;
            return (value ?? defaultValue);
        }

        /// <summary>
        /// Get the value of the attribute with the given name, cast to the designated type
        /// </summary>
        public static T GetAttribute<T>(this XElement xe, string attributeName, T defaultValue = default(T))
        {
            var attr = xe.Attribute(attributeName);
            if (attr == null) return defaultValue;
            var value = attr.Value;
            if (typeof(T) != typeof(string) && value == "") return defaultValue;
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
