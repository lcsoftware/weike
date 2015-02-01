/**  版本信息模板在安装目录下，可自行修改。
* TestCheck.cs
*
* 功 能： N/A
* 类 名： TestCheck
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:23:46   N/A    初版
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
	/// 流水批阅设置
	/// </summary>
	[Serializable]
	public partial class TestCheck
	{
        public int TestCheckID { get; set; }

        /// <summary>
        /// 测试编号
        /// </summary>
        public int TestID { get; set; }

        /// <summary>
        /// 批阅人编号
        /// </summary>
        public int UserID { get; set; }


        /// <summary>
        /// 按习题还是教学班   TeachingClass  Exercise
        /// </summary>
        public string  Source { get; set; }

        /// <summary>
        /// 教学班编号、习题编号
        /// </summary>
        public int SourceID { get; set; }


	}
}

