using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举项Attribute
        /// </summary>
        /// <typeparam name="T">自定义Attribute</typeparam>
        /// <param name="source">枚举</param>
        /// <returns>返回枚举</returns>
        public static T GetCustomAttribute<T>(Enum source) where T : Attribute
        {
            Type sourceType = source.GetType();
            string sourceName = Enum.GetName(sourceType, source);
            FieldInfo field = sourceType.GetField(sourceName);
            object[] attributes = field.GetCustomAttributes(typeof(T), false);

            return attributes.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// 获取DescriptionAttribute描述信息
        /// </summary>
        /// <param name="source">枚举</param>
        /// <returns>返回Description标记描述信息</returns>
        public static string GetDescription(Enum source)
        {
            var attr = GetCustomAttribute<DescriptionAttribute>(source);
            if (attr == null)
            {
                return null;
            }

            return attr.Description;
        }

        /// <summary>
        /// 获取DescriptionAttribute描述信息
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string GetEnumDesc<TType>(TType enumType)
        {
            FieldInfo[] fieldinfo = enumType.GetType().GetFields();
            DescriptionAttribute des = null;
            foreach (FieldInfo item in fieldinfo)
            {
                Object[] obj = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (obj.Length != 0)
                {
                    des = (DescriptionAttribute)obj[0];
                }
            }
            if (des != null)
            {
                return des.Description;
            }
            return null;
        }
    }
}
