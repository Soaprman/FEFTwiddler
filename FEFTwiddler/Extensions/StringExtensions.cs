namespace FEFTwiddler.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Get the rightmost <paramref name="maxLength"/> characters of this string
        /// </summary>
        public static string Right(this string str, int maxLength)
        {
            if (str.Length <= maxLength) return str;
            return str.Substring(str.Length - maxLength);
        }
    }
}
