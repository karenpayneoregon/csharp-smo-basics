using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Text.RegularExpressions.Regex;

namespace NuGetPackageHelpers.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveDoubleSpacings(this string sender, string replaceWith = "")   
        {
            var options = RegexOptions.None;
            var regex = new Regex("[ ]{2,}", options);
            sender = regex.Replace(sender, replaceWith);
            return sender;
        }
    }
}
