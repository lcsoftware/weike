using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    /// <summary>
    /// 文件对应的章节和知识点
    /// </summary>
    [Serializable]
    public partial class FileChapterKen
    {
        public FileChapterKen(){ }

        #region Model
        private int _chapterid;
        private int _kenid;

        public int ChapterID
        {
            get { return _chapterid; }
            set { _chapterid = value; }
        }
        public int KenID
        {
            get { return _kenid; }
            set { _kenid = value; }
        }
        #endregion
    }
}
