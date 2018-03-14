using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Util
{
    public static class QueryStringFormat
    {
        public static string BuilderQueryString<T>(string parameter, List<T> values)
        {
            string QueryString = string.Empty;
            var FormattedValue = values.Select(x => string.Format("{0}={1}&", parameter, x)).ToList();
            foreach (var formattedValue in FormattedValue)
            {
                QueryString += formattedValue;
            }
            return QueryString;
        }
        public static string BuilderQueryString<T>(List<T> values)
        {
            string QueryString = string.Empty;
            var FormattedValue = values.Select(x => string.Format("{0}/", x)).ToList();
            foreach (var formattedValue in FormattedValue)
            {
                QueryString += formattedValue;
            }
            return QueryString;
        }
    }
}
