﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.G2S.Resource.IBLL
{
    public interface IKenBLL
    {

        bool Ken_Del( Ken model );

        Ken Ken_ADD(Ken model);

        bool Ken_Upd(Ken model);
    }
}