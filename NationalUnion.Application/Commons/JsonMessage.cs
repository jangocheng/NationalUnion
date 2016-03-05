using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    public class JsonMessage
    {
        public JsonMessage(bool success, string message)
        {
            this.Res = success;
            this.Msg = message;
        }

        public bool Res { get; set; }

        public string Msg { get; set; }
    }
}
