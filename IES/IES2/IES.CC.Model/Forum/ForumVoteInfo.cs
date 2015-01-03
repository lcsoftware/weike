using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Forum.Model
{

    public  class ForumVoteInfo
    {
        /// <summary>
        /// 投票的问题列表信息
        /// </summary>
        public List<ForumVote> forumvotelist { get; set; }

        /// <summary>
        /// 问题选项列表
        /// </summary>
        public List<ForumVoteItem>  voteitemlist { get; set; }



        public List<ForumVoteResponse> responselist { get; set; }

    }


}
