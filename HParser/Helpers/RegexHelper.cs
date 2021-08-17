using System.Text.RegularExpressions;

namespace HParser
{
    internal static class RegexHelper
    {
        public static bool Test(string pattern, out Regex regex)
        {
            try
            {
                regex = new Regex(pattern);
                return true;
            }
            catch
            {
                regex = null;
                return false;
            }
        }
        public static bool Test(string pattern, RegexOptions options, out Regex regex)
        {
            try
            {
                regex = new Regex(pattern, options);
                return true;
            }
            catch
            {
                regex = null;
                return false;
            }
        }
    }
}
