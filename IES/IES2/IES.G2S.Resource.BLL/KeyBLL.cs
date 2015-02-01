using IES.G2S.Resource.DAL;
using IES.G2S.Resource.IBLL;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.Resource.BLL
{
    public class KeyBLL:IKeyBLL
    {

        public List<IES.Resource.Model.Key> Key_List(IES.Resource.Model.Key model)
        {
            return KeyDAL.Key_List(model);
        }

        /// <summary>
        /// 获取文件、习题相关有效关键字 
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <param name="Source"></param>
        /// <param name="UserID"></param>
        /// <param name="TopNum"></param>
        /// <returns></returns>
        public List<Key> ExerciseOrFile_Key_List(string SearchKey, string Source, int UserID, int TopNum,int OCID)
        {
            return KeyDAL.ExerciseOrFile_Key_List(SearchKey, Source, UserID, TopNum,OCID);
        }

        public List<IES.Resource.Model.Key> Resource_Key_List(string searchKey, string source, int userId, int topNum)
        {
            return KeyDAL.Resource_Key_List(searchKey, source, userId, topNum);
        }
        
    }
}
