using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// 异常集合类
    /// </summary>
    public class ValidationError
    {
        public ValidationError()
        {
        }

        public string ErrorMessage { get; set; }
    }

    public class ValidationErrors : List<ValidationError>
    {
        /// <summary>
        /// 添加错误信息描述
        /// </summary>
        /// <param name="errorMessage"></param>
        public void Add(string errorMessage)
        {
            base.Add(new ValidationError {ErrorMessage = errorMessage});
        }

        /// <summary>
        /// 获取错误集合
        /// </summary>
        public string Error
        {
            get
            {
                string error = "";
                this.All(a =>
                {
                    error += a.ErrorMessage;
                    return true;
                });

                return error;
            }
        }
    }
}
