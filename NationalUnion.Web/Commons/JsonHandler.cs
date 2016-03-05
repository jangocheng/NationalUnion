using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalUnion.Web.Commons
{
    public class JsonHandler
    {
        public static JsonMessageInfo CreateMessage(int type, string message, string value)
        {
            var json = new JsonMessageInfo
            {
                Type = type,
                Message = message,
                Value = value
            };

            return json;
        }

        public static JsonMessageInfo CreateMessage(int type, string message)
        {
            var json = new JsonMessageInfo
            {
                Type = type,
                Message = message
            };

            return json;
        }
    }

    public class JsonMessageInfo
    {
        public int Type { get; set; }

        public string Message { get; set; }

        public string Value { get; set; }
    }
}