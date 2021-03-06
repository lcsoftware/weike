﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Cache;
using IES.Resource.Model;
using IES.G2S.Resource.DAL;

namespace IES.G2S.Resource.BLL
{
    public  class AttachmentBLL
    {
        public List<Attachment> Attachment_List(Attachment model)
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "Attachment" + model.Source ))
            {
                List<Attachment> attachmentlist = AttachmentDAL.Attachment_List(model);
                cache.Set(string.Empty, "Attachment" + model.Source , attachmentlist);
                return attachmentlist;
            }
            else
            {
                return cache.Get<List<Attachment>>(string.Empty, "Attachment" + model.Source);
            }
        }



        public Attachment Attachment_Get(string FileID)
        {
            return AttachmentDAL.Attachment_Get(FileID);
        }


        public  bool Attachment_ADD(Attachment model)
        {
            return AttachmentDAL.Attachment_ADD(model);
        }

        public static bool Attachment_SourceID_Upd(Attachment model)
        {
            return AttachmentDAL.Attachment_SourceID_Upd(model);
        }


        public static bool Attachment_Del(Attachment model)
        {
            return AttachmentDAL.Attachment_Del(model);
        }


    }
}
