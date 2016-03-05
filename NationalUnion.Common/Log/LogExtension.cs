using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using log4net.Layout.Pattern;
using log4net.Layout;
using log4net.Util;


namespace NationalUnion.Common.Log
{
    public class NationalUnionPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {

            object obj = loggingEvent.MessageObject;
            if (obj == null)
            {
                writer.Write(SystemInfo.NullText);
                return;
            }
            if (Option == null)
            {
                writer.Write(obj.ToString());
                return;
            }
            if (Option != null)
            {
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            //throw new NotImplementedException();
        }

        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            Type type = loggingEvent.MessageObject.GetType();
            PropertyInfo propertyInfo = type.GetProperty(property);        
            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);

            return propertyValue;
        }

    }


    public class NationalUnionLayout : PatternLayout
    {
        public NationalUnionLayout()
        {
            this.AddConverter("o", typeof(NationalUnionPatternConverter));
        }
    }

    public class NationalUnionMemberLayout : PatternLayout
    {
        public NationalUnionMemberLayout()
        {
            this.AddConverter("property", typeof(NationalUnionPatternConverter));
        }
    }
}