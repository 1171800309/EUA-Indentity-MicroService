using EUA.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EUA.Domain.Domain
{
    [Table("sys_user")]
    public class sys_user:Entity
    {
        [Column]
        public string ID { get; set; }
        [Column]
        public string UserName { get; set; }
        [Column]
        public int Sex { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string LoignName { get; set; }
        [Column]
        public string PWD { get; set; }
        [Column]
        public string Phone { get; set; }
        [Column]
        public string TEL { get; set; }
        public int IsManager { get; set; }
        [Column]
        public string DeptCode { get; set; }
        [Column]
        public string Status { get; set; }
        [Column]
        public DateTime JoinDT { get; set; }
        [Column]
        public DateTime TerminationDT { get; set; }
        [Column]
        public string IDCard { get; set; }
    }
}
