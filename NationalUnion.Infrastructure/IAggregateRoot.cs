using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Infrastructure
{
    /// <summary>
    /// 用于标识一个实体为聚合根，IAggregateRoot接口用作泛型约束
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
    }
}
