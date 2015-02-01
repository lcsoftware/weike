using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    /// <summary>
    /// 常见问题表
    /// </summary>
   public  class FAQ
    {
       public int ID { get; set; }

       /// <summary>
       /// 子系统编号
       /// </summary>
       public int SysID { get; set; }


       /// <summary>
       /// 问题适用的用户类型
       /// </summary>
       public int UserType { get; set; }

       /// <summary>
       /// faq标题
       /// </summary>
       public string Title { get; set; }

       /// <summary>
       /// FAQ内容
       /// </summary>
       public string Conten { get; set; }

       /// <summary>
       /// 更新时间
       /// </summary>
       public DateTime UpdateTime { get; set; }

       /// <summary>
       /// 问题创建人
       /// </summary>
       public int UserID { get; set; }

       /// <summary>
       /// 创建人姓名
       /// </summary>
       public string  UserName { get; set; }

    }
}
