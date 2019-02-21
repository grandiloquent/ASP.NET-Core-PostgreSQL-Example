using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication.Shared
{
    public static class GenericExtensions
    {
        public static string ConcatenateLines(this IEnumerable<string> strings)
        {
            return strings.Concatenate((builder, nextValue) => builder.AppendLine(nextValue));
        }

        private static string Concatenate(this IEnumerable<string> strings,
            Func<StringBuilder, string, StringBuilder> builderFunc)
        {
            return strings.Aggregate(new StringBuilder(), builderFunc).ToString();
        }
    }
}