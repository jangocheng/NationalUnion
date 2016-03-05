using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace NationalUnion.Infrastructure
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    /// Refactored by jiangew 2014.06.10
    public class CustormUnityDependencyResolver
    {
        private const string HttpContextKey = "perRequestContainer";

        private static IUnityContainer _container;
        public static IUnityContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        static CustormUnityDependencyResolver()
        {
            _container = new UnityContainer();
            _container.LoadConfiguration();
        }

        /// <summary>
        /// 构造函数依赖注入
        /// </summary>
        /// <typeparam name="TRespository">泛型约束，Entity必须为一个类，且必须继承接口IRepository</typeparam>
        /// <typeparam name="TEntity">泛型约束，Entity必须为一个类，且必须继承接口IEntity</typeparam>
        /// <returns></returns>
        public static TRespository GetRepository<TRespository, TEntity>() where TRespository : class ,IRepository<TEntity>
            where TEntity : class,IEntity
        {
            return _container.Resolve<TRespository>();
        }

        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                return ChildContainer.Resolve(serviceType);
            }

            return IsRegistered(serviceType) ? ChildContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (IsRegistered(serviceType))
            {
                yield return ChildContainer.Resolve(serviceType);
            }

            foreach (var service in ChildContainer.ResolveAll(serviceType))
            {
                yield return service;
            }
        }

        protected IUnityContainer ChildContainer
        {
            get
            {
                var childContainer = HttpContext.Current.Items[HttpContextKey] as IUnityContainer;

                if (childContainer == null)
                {
                    HttpContext.Current.Items[HttpContextKey] = childContainer = _container.CreateChildContainer();
                }

                return childContainer;
            }
        }

        public static void DisposeOfChildContainer()
        {
            var childContainer = HttpContext.Current.Items[HttpContextKey] as IUnityContainer;

            if (childContainer != null)
            {
                childContainer.Dispose();
            }
        }

        private bool IsRegistered(Type typeToCheck)
        {
            var isRegistered = true;

            if (typeToCheck.IsInterface || typeToCheck.IsAbstract)
            {
                isRegistered = ChildContainer.IsRegistered(typeToCheck);

                if (!isRegistered && typeToCheck.IsGenericType)
                {
                    var openGenericType = typeToCheck.GetGenericTypeDefinition();

                    isRegistered = ChildContainer.IsRegistered(openGenericType);
                }
            }

            return isRegistered;
        }
    }
}
