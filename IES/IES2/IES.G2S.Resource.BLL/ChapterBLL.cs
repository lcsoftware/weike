﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.Resource.DAL;
using IES.Resource.Model;
using IES.AOP.G2S;
using IES.G2S.Resource.IBLL;

namespace IES.G2S.Resource.BLL
{
    public class ChapterBLL:IChapterBLL 
    {
        public bool Chapter_Del(Chapter model)
        {
            return ChapterDAL.Chapter_Del(model);
        }

        public bool Chapter_ADD(Chapter model)
        {
            return ChapterDAL.Chapter_ADD(model);
        }

        public bool Chapter_Upd(Chapter model)
        {
            return ChapterDAL.Chapter_Upd(model);
        }

    }
}