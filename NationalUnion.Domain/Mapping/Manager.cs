using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;

namespace NationalUnion.Domain.Mapping
{
    /// <summary>
    /// 渠道负责人
    /// </summary>
    public class Manager : IAggregateRoot
    {
        /// <summary>
        /// 负责人编号
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 ManagerId { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(100, MinimumLength = 2)]
        public string ManagerName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string ManagerEmail { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 渠道类型Id
        /// </summary>
        public int ChannelType { get; set; }

        /// <summary>
        /// 渠道Id
        /// </summary>
        public Int64 ChannelId { get; set; }

        /// <summary>
        /// 分享宝渠道Id
        /// </summary>
        public Int64 ShareChannelId { get; set; }

        /// <summary>
        /// 渠道负责人城市
        /// </summary>
        public int CId { get; set; }

        /// <summary>
        /// 提现密码
        /// </summary>
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string CashOutPwd { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// 账户状态
        /// </summary>
        public int IsActive { get; set; }

        /// <summary>
        /// 详细信息
        /// </summary>
        [StringLength(1000)]
        public string ManagerInfo { get; set; }

        public virtual Channel Channel { get; set; }

        public virtual ShareChannel ShareChannel { get; set; }

        public virtual City City { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
