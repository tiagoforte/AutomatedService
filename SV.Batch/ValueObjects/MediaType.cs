using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.ValueObjects
{
    public class MediaType
    {
        private MediaType(string value) { Value = value; }
        public string Value { get; set; }

        public static MediaType Json { get { return new MediaType("application/json"); } }
        public static MediaType FormUrlencoded { get { return new MediaType("application/x-www-form-urlencoded"); } }
        public static MediaType Html { get { return new MediaType("text/html"); } }
        public static MediaType Text { get { return new MediaType("text"); } }
    }
}
