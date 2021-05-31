using System;
using System.ComponentModel;
using System.Text;

namespace Previgesst
{
    public static class StringExtensions
    {
        public static T? ToNullable<T>(this string s) where T : struct
        {
            var result = new T?();
            try
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    var conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch
            {
                //Do nothing
            }

            return result;
        }

        public static string Left(this string str, int length)
        {
            str = (str ?? string.Empty);
            return str.Substring(0, Math.Min(length, str.Length));
        }

        public static string Right(this string str, int length)
        {
            str = (str ?? string.Empty);
            return (str.Length >= length)
                ? str.Substring(str.Length - length, length)
                : str;
        }

        public static string FlushNA( this string str)
        {
            str = (str ?? string.Empty);
            return (str.Trim() == "N/A" ? "" : str.Trim());
        }
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_' || c=='.')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
