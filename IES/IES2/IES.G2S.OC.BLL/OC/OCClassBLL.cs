using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.OC.IBLL.OC;
using IES.CC.OC.Model;
using IES.G2S.OC.DAL;
using IES.Security;
using IES.Cache;
using IES.G2S.OC.DAL.OC;
using IES.Common;

namespace IES.G2S.OC.BLL.OC
{
    public class OCClassBLL : IOCClassBLL
    {
        #region IOCClassBLL 成员

        #region  列表


        /// <summary>
        /// 教学班列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="TeamID"></param>
        /// <param name="Searchkey"></param>
        /// <param name="IsHistroy"></param>
        /// <returns></returns>
        public List<OCClass> OCClass_List(OCClass model, int PageIndex, int PageSize)
        {
            return OCClassDAL.OCClass_List(model, PageIndex, PageSize);
        }
        /// <summary>
        /// 教学班学生列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<OCClassStudent> OCClassStudent_List(OCClass model, int PageIndex, int PageSize)
        {
            return OCClassDAL.OCClassStudent_List(model, PageIndex, PageSize);
        }
        /// <summary>
        /// 教学班添加学生搜索
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<OCClassStudent> OCClass_Student_List(OCClassStudent model, int PageIndex, int PageSize)
        {

            return OCClassDAL.OCClass_Student_List(model, PageIndex, PageSize);



        }
        /// <summary>
        /// 教学班授课教师
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<OCTeamDropdownList> OCTeam_Dropdown_List(OCTeamDropdownList model)
        {
            return OCClassDAL.OCTeam_Dropdown_List(model);
        }
        /// <summary>
        /// 获取行政班列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<IES.JW.Model.Class> Class_List(IES.JW.Model.Class model, int PageIndex, int PageSize)
        {
            return OCClassDAL.Class_List(model, PageIndex, PageSize);
        }
        /// <summary>
        /// 通过行政班获取学生信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<OCClassStudent> ClassStudent_List(IES.JW.Model.Class model, List<OCClassStudent> occlassstudentlist, int PageIndex, int PageSize)
        {
            List<OCClassStudent> occlassstudenttemp = OCClassDAL.ClassStudent_List(model, PageIndex, PageSize);

            List<OCClassStudent> occlassstudentlistTemp = new List<OCClassStudent>();

            if (occlassstudentlist == null)
            {
                occlassstudentlistTemp = occlassstudenttemp;
            }
            else
            {
                occlassstudentlistTemp = occlassstudentlist.Union(occlassstudenttemp, new ProductComparer()).ToList();
            }
            for (int i = 0; i < occlassstudentlistTemp.Count; i++)
            {
                occlassstudentlistTemp[i].rownum = i + 1;
                occlassstudentlistTemp[i].RowsCount = occlassstudentlistTemp.Count;
            }
            return occlassstudentlistTemp;
        }
        /// <summary>
        /// 通过搜索添加学生
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<OCClassStudent> SelectSelectedStudent_List(List<OCClassStudent> occlassstudentsearchlist, List<OCClassStudent> occlassstudentlist)
        {
            List<OCClassStudent> occlassstudentlistTemp = new List<OCClassStudent>();
            if (occlassstudentlist == null)
            {
                occlassstudentlistTemp = occlassstudentsearchlist.FindAll(o => o.IsSelected == true).ToList();
            }
            else
            {
                occlassstudentlistTemp = occlassstudentlist.Union(occlassstudentsearchlist.FindAll(o => o.IsSelected == true), new ProductComparer()).ToList();
            }


            for (int i = 0; i < occlassstudentlistTemp.Count; i++)
            {
                occlassstudentlistTemp[i].rownum = i + 1;
                occlassstudentlistTemp[i].RowsCount = occlassstudentlistTemp.Count;
            }
            return occlassstudentlistTemp;


        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<OCClassStudent> SelectAll_List(List<OCClassStudent> occlassstudentsearchlist, bool isselectall)
        {
            for (int i = 0; i < occlassstudentsearchlist.Count; i++)
            {
                occlassstudentsearchlist[i].IsSelected = isselectall;
            }
            return occlassstudentsearchlist;

        }

        /// <summary>
        /// 单页全选
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<OCClassStudent> Select_Page_List(List<OCClassStudent> occlassstudentsearchlist, bool isselectall, int PageIndex, int PageSize)
        {
            for (int i = 0; i < occlassstudentsearchlist.Count; i++)
            {
                //item.rownum <= num * size && item.rownum >= ((num - 1) * size + 1)
                if ((occlassstudentsearchlist[i].rownum <= PageIndex * PageSize) && (occlassstudentsearchlist[i].rownum >= ((PageIndex - 1) * PageSize + 1)))
                {
                    occlassstudentsearchlist[i].IsSelected = isselectall;
                }
            }
            return occlassstudentsearchlist;

        }
        /// <summary>
        /// 获取自己相关的教学班列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public List<IES.JW.Model.TeachingClass> TeachingClass_Owner_List(int UserID, int OCID)
        {
            return OCClassDAL.TeachingClass_Owner_List(UserID, OCID);
        }



        #endregion


        #region 详细信息

        public OCClassInfo OCClassInfo_Get(OCClass model, int PageIndex, int PageSize)
        {
            OCClassInfo occlassinfo = new OCClassInfo();
            occlassinfo.OcClass = OCClassDAL.OCClass_Get(model);
            occlassinfo.OcClassStudent = OCClassDAL.OCClassStudent_List(model, PageIndex, PageSize);

            return occlassinfo;
        }
        /// <summary>
        /// 获取教学班基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OCClass OCClass_Get(OCClass model)
        {
            return OCClassDAL.OCClass_Get(model);

        }
        #endregion


        #region  新增

        public OCClass OCClass_Edit(OCClass model)
        {
            return OCClassDAL.OCClass_Edit(model);
        }
        public OCClass OCClass_Edit(OCClass model, List<OCTeamDropdownList> octeamdropdownlist, List<OCClassStudent> occlassstudent)
        {
            //model.OcClassStudent
            model.TeacherID = octeamdropdownlist.First(oc => oc.IsSelected == true).UserID;

            model.TeacherIDS = ListHelp.GetPropertyValues(octeamdropdownlist.FindAll(oc => oc.IsSelected == true), "UserID");
            if (occlassstudent != null)
            {
                model.StudentIDS = ListHelp.GetPropertyValues(occlassstudent, "UserID");
            }
            return OCClassDAL.OCClass_Edit(model);
        }

        #endregion


        #region 对象更新
        public bool OCClass_IsHistroy_Upd(OCClass model)
        {
            return OCClassDAL.OCClass_IsHistroy_Upd(model);
        }
        public bool OCClass_RegNum_Upd(OCClass model)
        {
            return OCClassDAL.OCClass_RegNum_Upd(model);
        }
        public bool OCClass_RecruitStatus_Upd(OCClass model)
        {
            return OCClassDAL.OCClass_RecruitStatus_Upd(model);
        }

        #endregion


        #region 单个批量更新




        #endregion



        #region 删除
        public bool OCClass_Del(OCClass model)
        {
            return OCClassDAL.OCClass_Del(model);
        }

        public bool OCClass_Batch_Del(string OCClassIDs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="occlassstudent"></param>
        /// <param name="occlassstudentlist"></param>
        /// <returns></returns>
        public List<OCClassStudent> DeleteStudent(OCClassStudent occlassstudent, List<OCClassStudent> occlassstudentlist)
        {
            return occlassstudentlist.FindAll(oc => oc.UserID != occlassstudent.UserID);
        }
        /// <summary>
        /// 批量删除学生
        /// </summary>
        /// <param name="occlassstudentdellist"></param>
        /// <param name="octeamdropdownlist"></param>
        /// <returns></returns>
        public List<OCClassStudent> BatchDeleteStudent(List<OCClassStudent> octeamdropdownlist)
        {
            return octeamdropdownlist.FindAll(oc => oc.IsSelected == false);
        }

        #endregion
        #endregion
    }
    public class ProductComparer : IEqualityComparer<OCClassStudent>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(OCClassStudent x, OCClassStudent y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;
            //Check whether the products' properties are equal.
            return x.UserID == y.UserID && x.UserNo == y.UserNo;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.
        public int GetHashCode(OCClassStudent product)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(product, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = product.UserNo == null ? 0 : product.UserNo.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = product.UserID.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }

    }

}

