using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalUnion.Domain.Mapping
{
    /// <summary>
    /// 会员
    /// </summary>
    public class User : IAggregateRoot
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        //[Required]
        [StringLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public string RealName { get; set; }

        /// <summary>
        /// 归属渠道ID
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ManagerID { get; set; }

        /// <summary>
        /// 达人用户级别
        /// </summary>
        public int UserLevelID { get; set; }

        /// <summary>
        /// VIP级别
        /// </summary>
        public int UserVip { get; set; }

        /// <summary>
        /// VIP级别过期时间
        /// </summary>
        public DateTime VipExpirationTime { get; set; }

        /// <summary>
        ///  注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 用户信息({手机号PhoneNo,身份证号IDNo，邮箱EMail,QQ,微信Weixin,银行卡信息号BankCardInfo{开户行BankName,CardNo}})
        /// </summary>
        [StringLength(1000)]
        public string UserInfo { get; set; }

        /// <summary>
        ///  绑定平台信息（如QQ，weibo平台的信息,帐号、AccessToken等）
        /// </summary>
        [StringLength(2000)]
        public string BindingInfo { get; set; }

        /// <summary>
        /// 可提现现金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal Cash { get; set; }

        /// <summary>
        /// 冻结现金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal CashFreeze { get; set; }

        /// <summary>
        /// 金币
        /// </summary>
        public int Coin { get; set; }

        /// <summary>
        /// 冻结金币
        /// </summary>
        public int CoinFreeze { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [StringLength(100)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 用户类别
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string DepartMent { get; set; }

        /// <summary>
        /// 所属中心
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        /// 国美员工编号
        /// </summary>
        public string GomeNo { get; set; }

        /// <summary>
        /// 渠道负责人
        /// </summary>
        public virtual Channel Channel { get; set; }

        /// <summary>
        /// 用户级别
        /// </summary>
        [ForeignKey("UserLevelID")]
        public virtual UserLevel UserLevel { get; set; }

        [NotMapped]
        public Guid Id { get; set; }
    }
}
     