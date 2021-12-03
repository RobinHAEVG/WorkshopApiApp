using System;
using System.Text;

namespace WorkshopApi
{
    public static class Extensions
    {
        public static string ToBase64(this string value)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string FromBase64(this string value)
        {
            byte[] valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}
