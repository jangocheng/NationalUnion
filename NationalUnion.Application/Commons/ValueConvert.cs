using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    public static partial class ValueConvert
    {
        /// <summary>
        /// 使用MD5加密字符串
        /// </summary>
        /// <param name="str">待加密的字符</param>
        /// <returns></returns>
        public static string EncryptByMd5(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            // 获取加密服务
            var md5 = new MD5CryptoServiceProvider();
            // 获取要加密的字符串，并转换为Byte[]数组
            byte[] arr = Encoding.UTF8.GetBytes(str);
            // 加密Byte[]数组
            byte[] bytes = md5.ComputeHash(arr);
            // 将加密后的数组转换为字符串
            str = BitConverter.ToString(bytes);
            return str;
        }

        public static string Encrypt(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            // 获取要加密的字符串，并转换为Byte[]数组
            byte[] res = Encoding.UTF8.GetBytes(str);
            // 获取加密服务
            var md5 = new MD5CryptoServiceProvider();
            // 加密Byte[]数组
            byte[] md5Encr = md5.ComputeHash(res);
            // 将加密后的数组转换为字符串
            str = Encoding.UTF8.GetString(md5Encr);

            return str;
        }

        /// <summary>
        /// 将最后一个字符串的路径path替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Path(this string str, string path)
        {
            int index = str.LastIndexOf('\\');
            int indexDian = str.LastIndexOf('.');

            return str.Substring(0, index + 1) + path + str.Substring(indexDian);
        }

        public static List<string> ToList(this string ids)
        {
            var listId = new List<string>();
            if (!string.IsNullOrEmpty(ids))
            {
                var sort = new SortedSet<string>(ids.Split(','));
                listId.AddRange(sort);
            }

            return listId;
        }

        /// <summary>
        /// 从^分割的字符串中获取多个Id,先是用 ^ 分割，再使用 & 分割
        /// </summary>
        /// <param name="ids">先是用 ^ 分割，再使用 & 分割</param>
        /// <returns></returns>
        public static List<string> GetIdSort(this string ids)
        {
            var listId = new List<string>();
            if (!string.IsNullOrEmpty(ids))
            {
                var sort = new SortedSet<string>(ids.Split('^')
                    .Where(w => !string.IsNullOrWhiteSpace(w) && w.Contains('&'))
                    .Select(s => s.Substring(0, s.IndexOf('&'))));
                listId.AddRange(sort);
            }

            return listId;
        }

        /// <summary>
        /// 从[，]分割的字符串中获取单个Id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string GetId(this string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var sort = new SortedSet<string>(ids.Split('^')
                    .Where(w => !string.IsNullOrWhiteSpace(w) && w.Contains('&'))
                    .Select(s => s.Substring(0, s.IndexOf('&'))));

                return sort.FirstOrDefault(item => !string.IsNullOrWhiteSpace(item));
            }
            return null;
        }

        /// <summary>
        /// 将String转换为Dictionary类型，过滤掉为空的值，首先 6 分割，再 7 分割
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, string> StringToDictionary(string value)
        {
            var queryDictionary = new Dictionary<string, string>();
            string[] s = value.Split('^');
            for (int i = 0; i < s.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(s[i]) && !s[i].Contains("undefined"))
                {
                    var ss = s[i].Split('&');
                    if ((!string.IsNullOrEmpty(ss[0])) && (!string.IsNullOrEmpty(ss[1])))
                    {
                        queryDictionary.Add(ss[0], ss[1]);
                    }
                }

            }
            return queryDictionary;
        }

        /// <summary>
        /// 得到对象的 Int 类型的值，默认值0
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>如果对象的值可正确返回，返回对象转换的值，否则，返回默认值0</returns>
        public static int GetInt(this object value)
        {
            return GetInt(value, 0);
        }

        /// <summary>
        /// 得到对象的 Int 类型的值，默认值0
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值，否则，返回默认值0</returns>
        public static int GetInt(this object value, int defaultValue)
        {

            if (value == null) return defaultValue;
            if (value is string && value.GetString().HasValue() == false) return defaultValue;

            if (value is DBNull) return defaultValue;

            if ((value is string) == false && (value is IConvertible) == true)
            {
                return (value as IConvertible).ToInt32(CultureInfo.CurrentCulture);
            }

            int retVal = defaultValue;
            if (int.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, out retVal))
            {
                return retVal;
            }
            else
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// 得到对象的 String 类型的值，默认值string.Empty
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值string.Empty</returns>
        public static string GetString(this object value)
        {
            return GetString(value, string.Empty);
        }

        /// <summary>
        /// 得到对象的 String 类型的值，默认值string.Empty
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值 。</returns>
        public static string GetString(this object value, string defaultValue)
        {
            if (value == null) return defaultValue;
            string retVal = defaultValue;
            try
            {
                var strValue = value as string;
                if (strValue != null)
                {
                    return strValue;
                }

                char[] chrs = value as char[];
                if (chrs != null)
                {
                    return new string(chrs);
                }

                retVal = value.ToString();
            }
            catch
            {
                return defaultValue;
            }
            return retVal;
        }

        /// <summary>
        /// 得到对象的 DateTime 类型的值，默认值为DateTime.MinValue
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回的默认值为DateTime.MinValue </returns>
        public static DateTime GetDateTime(this object value)
        {
            return GetDateTime(value, DateTime.MinValue);
        }

        /// <summary>
        /// 得到对象的 DateTime 类型的值，默认值为DateTime.MinValue
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回默认值为DateTime.MinValue</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回的默认值为DateTime.MinValue</returns>
        public static DateTime GetDateTime(this object value, DateTime defaultValue)
        {
            if (value == null) return defaultValue;

            if (value is DBNull) return defaultValue;

            var strValue = value as string;
            if (strValue == null && (value is IConvertible))
            {
                return (value as IConvertible).ToDateTime(CultureInfo.CurrentCulture);
            }
            if (strValue != null)
            {
                strValue = strValue
                    .Replace("年", "-")
                    .Replace("月", "-")
                    .Replace("日", "-")
                    .Replace("点", ":")
                    .Replace("时", ":")
                    .Replace("分", ":")
                    .Replace("秒", ":")
                      ;
            }
            DateTime dt = defaultValue;
            if (DateTime.TryParse(value.GetString(), out dt))
            {
                return dt;
            }

            return defaultValue;
        }

        /// <summary>
        /// 得到对象的布尔类型的值，默认值false
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值false</returns>
        public static bool GetBool(this object value)
        {
            return GetBool(value, false);
        }

        /// <summary>
        /// 得到对象的 Bool 类型的值，默认值false
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值false</returns>
        public static bool GetBool(this object value, bool defaultValue)
        {
            if (value == null) return defaultValue;
            if (value is string && value.GetString().HasValue() == false) return defaultValue;

            if ((value is string) == false && (value is IConvertible) == true)
            {
                if (value is DBNull) return defaultValue;

                try
                {
                    return (value as IConvertible).ToBoolean(CultureInfo.CurrentCulture);
                }
                catch { }
            }

            if (value is string)
            {
                if (value.GetString() == "0") return false;
                if (value.GetString() == "1") return true;
                if (value.GetString().ToLower() == "yes") return true;
                if (value.GetString().ToLower() == "no") return false;
            }
            bool retVal = defaultValue;
            if (bool.TryParse(value.GetString(), out retVal))
            {
                return retVal;
            }
            else return defaultValue;
        }

        /// <summary>
        /// 检测 GuidValue 是否包含有效的值，默认值Guid.Empty
        /// </summary>
        /// <param name="guidValue">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值Guid.Empty</returns>
        public static Guid GetGuid(string guidValue)
        {
            try
            {
                return new Guid(guidValue);
            }
            catch { return Guid.Empty; }
        }

        /// <summary>
        /// 检测 Value 是否包含有效的值，默认值false
        /// </summary>
        /// <param name="value"> 传入的值</param>
        /// <returns> 包含，返回true，不包含，返回默认值false</returns>
        public static bool HasValue(this string value)
        {
            if (value != null)
            {
                return !string.IsNullOrEmpty(value.ToString(CultureInfo.InvariantCulture));
            }
            return false;
        }
    }
}
