using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Common.SqlHelper
{
    /// <summary>
    /// 数据访问辅助类[基于Procedure]
    /// </summary>
    public class ProcedureHelper
    {
        /// <summary>
        /// 根据存储过程参数列表，返回查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storeProduce"></param>
        /// <param name="sqlPara"></param>
        /// <returns></returns>
        public static IList<T> ExcuteDataSet<T>(string storeProduce, SqlParameter[] sqlPara)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand(storeProduce, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(sqlPara);
            SqlDataAdapter sad = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            try
            {
                cn.Open();
                sad.Fill(ds);
            }
            catch (Exception ex)
            {
                //cn.Dispose();
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Dispose();
            }
            return DataSetToList<T>(ds, 0);
        }

        /// <summary>
        /// 根据存储过程，返回查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storeProduce"></param>
        /// <returns></returns>
        public static IList<T> ExcuteDataSetNoParameters<T>(string storeProduce)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand(storeProduce, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddRange(sqlPara);
            SqlDataAdapter sad = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            try
            {
                cn.Open();
                sad.Fill(ds);
            }
            catch (Exception ex)
            {
                //cn.Dispose();
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Dispose();
            }
            return DataSetToList<T>(ds, 0);
        }

        /// <summary>
        /// 根据sql语句获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlstring"></param>
        /// <returns></returns>
        public static IList<T> ExcuteDataSetSql<T>(string sqlstring)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand(sqlstring, cn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                cn.Open();
                sad.Fill(ds);
            }
            catch (Exception ex)
            {
                //cn.Dispose();
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Dispose();
            }
            return DataSetToList<T>(ds, 0);
        }

        /// <summary>
        /// DataSetToList
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="dataSet">数据源</param>
        /// <param name="tableIndex">需要转换表的索引 </param>
        /// <returns>泛型集合</returns>
        private static IList<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
        {
            //确认参数有效
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
                return null;
            DataTable dt = dataSet.Tables[tableIndex];
            IList<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T t = Activator.CreateInstance<T>();

                //获取对象所有属性
                PropertyInfo[] propertyInfo = t.GetType().GetProperties();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                //info.SetValue(t, dt.Rows[i][j], null);
                                info.SetValue(t, ChangeType(dt.Rows[i][j], info.PropertyType, System.Globalization.CultureInfo.CurrentCulture));
                            }
                            else
                            {
                                info.SetValue(t, null, null);
                            }

                            break;
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// 增强版类型转换
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        private static object ChangeType(object obj, Type conversionType, IFormatProvider provider)
        {

            #region Nullable
            Type nullableType = Nullable.GetUnderlyingType(conversionType);
            if (nullableType != null)
            {
                if (obj == null)
                {
                    return null;
                }

                if (nullableType.IsEnum)
                {
                    return Enum.Parse(nullableType, obj.ToString());
                }
                else
                    return System.Convert.ChangeType(obj, nullableType, provider);
            }
            #endregion
            if (typeof(System.Enum).IsAssignableFrom(conversionType))
            {
                return Enum.Parse(conversionType, obj.ToString());
            }
            return System.Convert.ChangeType(obj, conversionType, provider);
        }

    }
}
