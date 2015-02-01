using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Forum.Model
{

    /// <summary>
    /// 论坛回复信息，回复列表，及我关注的情况
    /// </summary>
    public class ForumResponseInfo
    {
        public List<ForumResponse> forumresponselist { get; set; }

        public List<ForumMy> forummylist { get; set; }

    }
}
