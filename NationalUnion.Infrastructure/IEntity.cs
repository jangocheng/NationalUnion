using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Infrastructure
{
    /// <summary>
    /// 用于标识该类是一个实体
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
