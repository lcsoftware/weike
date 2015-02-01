using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace IES.Common
{
    /// <summary>
    /// 集合帮助类
    /// </summary>
    public static class ListHelp
    {
        /// <summary>
        /// 获取一个集合中指定的属性值,并将他们用分割符隔开,以字符串的形式返回
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="list">需要处理的集合</param>
        /// <param name="propertyName">属性的名称</param>
        /// <param name="splitChar">属性值分离的字符</param>
        /// <returns></returns>
        public static string GetPropertyValues<T>(IList<T> list, string propertyName, string splitChar = ",")
        {
            if (list == null)
                throw new ArgumentNullException("ListHelp.GetPropertyValues.list");

            if (propertyName == null)
                throw new ArgumentNullException("ListHelp.GetPropertyValues.propertyName");

            if (splitChar == null)
                throw new ArgumentNullException("ListHelp.GetPropertyValues.splitChar");

            if (list.Count == 0) 
                return "";

            Type type = typeof(T);
            PropertyInfo proInfo =  type.GetProperty(propertyName);
            if (proInfo == null)
                throw new Exception(string.Format("类型'{0}'没有找到属性名'{1}'", type.FullName, propertyName));

            StringBuilder propertyValues = new StringBuilder();
            T temp;
            for (int i = 0; i < list.Count; i++)
            {
                temp = list[i];
                if (temp != null)
                {
                    propertyValues.Append(proInfo.GetValue(temp));
                }
                if (i < list.Count - 1)
                    propertyValues.Append(splitChar);
            }

            return propertyValues.ToString();
        }

    }
}
 