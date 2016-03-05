using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Web;

namespace NationalUnion.Infrastructure
{
    public class NationalUnionResolver
    {
        private static IUnityContainer _container;
        public static IUnityContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        static NationalUnionResolver()
        {
            //加载指定文件的依赖注入
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            string vFilePath = HttpContext.Current.Server.MapPath("//Unity.config");
            fileMap.ExeConfigFilename = vFilePath;

            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection("unity");
            _container = new UnityContainer();
            _container.LoadConfiguration(section);
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

    }
}
