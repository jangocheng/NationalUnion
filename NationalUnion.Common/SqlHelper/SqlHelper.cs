using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;


namespace NationalUnion.Common.SqlHelper
{
    /// <summary>
    /// 数据访问辅助类[基于SQLServer]
    /// </summary>
    public abstract class SqlHelper
    {
        //数据库连接字符串[Web.config来配置]
        //<add key="NationalUnion" value="server=127.0.0.1;database=NationalUnion;uid=sa;pwd=jiangew" />
        private static readonly string ConnStr = ConfigurationManager.AppSettings["NationalUnion"];
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="sError"></param>
        /// <returns></returns>
        public static DataTable RunQuery(CommandType cmdType, string cmdText, SqlParameter[] commandParameters, out string sError)
        {
            sError = "";
            var ds = new DataSet();
            using (var conn = new SqlConnection(ConnStr))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                var da = new SqlDataAdapter {SelectCommand = cmd};
                da.Fill(ds);
            }
            if (ds.Tables.Count > 0) return ds.Tables[0];
            else return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argConStr"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="sError"></param>
        /// <returns></returns>
        public static DataTable RunQuery(string argConStr,CommandType cmdType, string cmdText, SqlParameter[] commandParameters, out string sError)
        {
            sError = "";
            var ds = new DataSet();
            using (var conn = new SqlConnection(argConStr))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                var da = new SqlDataAdapter {SelectCommand = cmd};
                da.Fill(ds);
            }
            if (ds.Tables.Count > 0) return ds.Tables[0];
            else return null;
        }

        /// <summary>
        /// 执行增,删,改的sql语句或者存储过程
        /// </summary>       
        /// <param name="cmdType">命令类型(存储过程,sql语句)</param>
        /// <param name="cmdText">命令文本(存储过程的名称,sql语句)</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="sError"></param>
        /// <returns>返回值</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, SqlParameter[] commandParameters, out string sError)
        {
            int R = 0;
            using (var conn = new SqlConnection(ConnStr))
            {
                var cmd = new SqlCommand();
                try
                {
                    PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                    cmd.ExecuteNonQuery();
                    sError = "";
                    R = (int)cmd.Parameters["Return Value"].Value;
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    R = -1;
                }
            }

            return R;
        }

        /// <summary>
        /// 执行查询(存储过程或sql语句)
        /// </summary>       
        /// <param name="cmdType">命令类型(存储过程,sql语句)</param>
        /// <param name="cmdText">命令文本(存储过程的名称,sql语句)</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="sError"></param>
        /// <returns>返回SqlDataReader的对象(只读只进的数据集)</returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, SqlParameter[] commandParameters, out string sError)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(ConnStr);
            try
            {
                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                sError = "";
                return rdr;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                conn.Close();
                return null;
            }
        }

        /// <summary>
        /// 执行查询(存储过程或sql语句)
        /// </summary>       
        /// <param name="cmdType">命令类型(存储过程,sql语句)</param>
        /// <param name="cmdText">命令文本(存储过程的名称,sql语句)</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="sError"></param>
        /// <returns>返回SqlDataReader的对象(只读只进的数据集)</returns>
        public static SqlDataReader ExecuteReaderPage(CommandType cmdType, string cmdText, SqlParameter[] commandParameters, out string sError)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(ConnStr);
            try
            {
                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                sError = "";
                return rdr;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                conn.Close();
                return null;
            }
        }

        /// <summary>
        /// 执行查询(存储过程或sql语句)
        /// </summary>       
        /// <param name="cmdType">命令类型(存储过程,sql语句)</param>
        /// <param name="cmdText">命令文本(存储过程的名称,sql语句)</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>返回查询结果的第一行第一列</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(ConnStr))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行查询(存储过程或sql语句)
        /// </summary>       
        /// <param name="argConStr"></param>
        /// <param name="cmdType">命令类型(存储过程,sql语句)</param>
        /// <param name="cmdText">命令文本(存储过程的名称,sql语句)</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>返回查询结果的第一行第一列</returns>
        public static object ExecuteScalar(string argConStr, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (var connection = new SqlConnection(argConStr))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        
        /// <summary>
        /// 构造SqlCommand对象
        /// </summary>
        /// <param name="cmd">需要修改的SqlCommand对象</param>
        /// <param name="conn">关联的连接</param>        
        /// <param name="cmdType">关联的命令类型</param>
        /// <param name="cmdText">关联的命令文本</param>
        /// <param name="cmdParms">关联的参数数组</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open) conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 600;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms) cmd.Parameters.Add(parm);
            }
            cmd.Parameters.Add(new SqlParameter("Return Value", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
        }

        //关于事务处理
        //开始事务
        public static void BeginTrans(SqlConnection conn, SqlTransaction trans)
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            if (trans == null)
            {
                trans = conn.BeginTransaction();
            }
        }

        //提交事务
        public static void CommitTrans(SqlConnection conn, SqlTransaction trans)
        {
            if (trans != null)
            {
                trans.Commit();
                conn.Close();
            }
        }

        //回滚事务
        public static void RollBackTrans(SqlConnection conn, SqlTransaction trans)
        {
            if (trans != null)
            {
                trans.Rollback();
                conn.Close();
            }
        }

        /// <summary>
        /// 执行增,删,改的sql语句或者存储过程
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="cmdType">命令类型(存储过程,sql语句)</param>
        /// <param name="cmdText">命令文本(存储过程的名称,sql语句)</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="sError"></param>
        /// <returns>成功影响的行数</returns>
        public static int ExecuteNonQueryTrans(SqlConnection conn, SqlTransaction trans, SqlCommand cmd, CommandType cmdType, string cmdText, SqlParameter[] commandParameters, out string sError)
        {
            //SqlCommand cmd = new SqlCommand();
            //cmd.Transaction = trans;
            int r = 0;
            try
            {
                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                cmd.ExecuteNonQuery();
                sError = "";
                r = (int)cmd.Parameters["Return Value"].Value;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                r = -1;
            }
            return r;
        }

        #region	参数
        /// <summary>
        /// 创建SqlParameter
        /// </summary>
        /// <param name="sParamName">参数名称</param>
        /// <param name="dbType">参数类型，SqlDbType枚举</param>
        /// <param name="size">参数大小</param>
        /// <param name="d">参数类型</param>
        /// <param name="v">参数值</param>
        /// <returns>返回创建好的SqlParameter</returns>
        private static SqlParameter MakeParam(string sParamName, SqlDbType dbType, Int32 size, ParameterDirection d, object v)
        {
            SqlParameter pa = size > 0 ? new SqlParameter(sParamName, dbType, size) : new SqlParameter(sParamName, dbType);

            pa.Direction = d;

            if (!(d == ParameterDirection.Output && v == null))
                pa.Value = v;

            return pa;
        }

        /// <summary>
        /// 创建传入参数
        /// </summary>
        /// <param name="sParamName">参数名</param>
        /// <param name="d">参数类型，为SqlDbType枚举</param>
        /// <param name="size">大小</param>
        /// <param name="v">参数值</param>
        /// <returns>返回创建好的输入参数</returns>
        public static SqlParameter MakeInParam(string sParamName, SqlDbType d, int size, object v)
        {
            return MakeParam(sParamName, d, size, ParameterDirection.Input, v);
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        /// <param name="sParamName">参数名</param>
        /// <param name="d">参数类型，为SqlDbType枚举成员</param>
        /// <param name="size">参数大小</param>
        /// <returns>返回创建好的输出参数</returns>
        public static SqlParameter MakeOutParam(string sParamName, SqlDbType d, int size)
        {
            return MakeParam(sParamName, d, size, ParameterDirection.Output, null);
        }
        #endregion

    }
}
