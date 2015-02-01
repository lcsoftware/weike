using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.JW.DAL;
using IES.JW.Model;
using IES.G2S.JW.IBLL;

namespace IES.G2S.JW.BLL
{
    public class MailServerBLL
    {

        #region 列表
        /// <summary>
        /// 获取列表
        /// </summary>
        public List<MailServer> MailServer_List()
        {
            return MailServerDAL.MailServer_List();
        }
        #endregion

        #region 编辑或新增
        /// <summary>
        /// 
        /// </summary>
        public MailServer MailServer_Edit(MailServer model)
        {
            return MailServerDAL.MailServer_Edit(model);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool MailServer_Del(MailServer model)
        {
            return MailServerDAL.MailServer_Del(model);
        }
        #endregion

    }
}
