

namespace App.Score.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 民族
    /// </summary>
    public class TdNation
    {
        public string SystemID { get; set; }
        public string NationName { get; set; }
        public string NationNo { get; set; }
    }
    /// <summary>
    /// 户口类别
    /// </summary>
    public class TdPolitic
    {
        public string SystemID { get; set; }
        public string PoliticsName { get; set; }
        public string PoliticsCode { get; set; }
    }

    /// <summary>
    /// 政治面貌
    /// </summary>
    public class TdResidenceType
    {
        public string SystemID { get; set; }
        public string ResidenceTypeName { get; set; }
        public string ResidenceType { get; set; }
    }


    public class ResultEntity
    {
        public int State;
        public string Context;
    }
}
