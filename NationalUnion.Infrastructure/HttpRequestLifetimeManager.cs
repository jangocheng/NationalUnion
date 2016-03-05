using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;

namespace NationalUnion.Infrastructure
{
    public class HttpRequestLifetimeManager : LifetimeManager
    {
        private readonly Guid _key;

        public HttpRequestLifetimeManager()
        {
            _key = Guid.NewGuid();
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[_key];
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[_key] = newValue;
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove(_key);
        }
    }

}