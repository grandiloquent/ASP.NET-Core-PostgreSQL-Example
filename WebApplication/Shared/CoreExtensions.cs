using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication.Shared
{
    public static class CoreExtensions
    {
        public static string DumpModelState
            (this ModelStateDictionary modelStateDictionary)
        {
            var sb = new StringBuilder();

            foreach (var (key, value) in modelStateDictionary)
            {
                sb.AppendLine($"{key} = {value.RawValue}");
                if (value.Children == null) continue;
                foreach (var c in value.Children)
                {
                    sb.AppendLine(c.ToString());
                }
            }

            return sb.ToString();
        }
    }
}