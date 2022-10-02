using EUA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EUA.Domain.Model
{
    [Table("sys_user")]
    public class Sys_User:Entity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Column]
        public string ID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [Column]
        public string UserName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column]
        public Gender Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column]
        public string Email { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        [Column]
        public string LoignName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Column]
        public string Phone { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        [Column]
        public string TEL { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        [Column]
        public string IsManager { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        [Column]
        public string DeptCode { get; set; }
        /// <summary>
        /// 状态（0，在职；1，锁定；2，离职）
        /// </summary>
        [Column]
        public UserState Status { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        [Column]
        public DateTime? JoinDT { get; set; }
        /// <summary>
        /// 解约时间（离职时间）
        /// </summary>
        [Column]
        public DateTime? TerminationDT { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        [Column]
        public string IDCard { get; set; }

    }

    public enum Gender
    { 
        /// <summary>
        /// 不明性别（不公布）
        /// </summary>
        Unknow = 0,
        /// <summary>
        /// 男性
        /// </summary>
        Man = 1,
        /// <summary>
        /// 女性
        /// </summary>
        Woman =2
    }

    public enum UserState
    {
        /// <summary>
        /// 在职（正常）
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 锁定
        /// </summary>
        Lock = 1,
        /// <summary>
        /// 离职
        /// </summary>
        Quite = 2
    }
}
