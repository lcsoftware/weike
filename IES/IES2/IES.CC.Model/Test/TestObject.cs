/**  版本信息模板在安装目录下，可自行修改。
* TestObject.cs
*
* 功 能： N/A
* 类 名： TestObject
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:23:47   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Test.Model
{
	/// <summary>
	/// 测试发放对象
	/// </summary>
	[Serializable]
	public partial class TestObject
	{
        public int TestObjectID { get; set; }
        public int OCID { get; set; }
        public int TestID { get; set; }
        public int TeachingClassID { get; set; }

        /// <summary>
        /// 教学班是否全选
        /// </summary>
        public bool IsALL { get; set; }


        /// <summary>
        /// 学生编号
        /// </summary>
        public string  IDS { get; set; }

	}
}

