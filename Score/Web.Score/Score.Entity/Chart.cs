
namespace App.Score.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Title
    {
        public string text = "";
        public string subtext = "";
        public string x = "center";
    }
    public class Legend
    {
        public string x = "left";
        public IList<string> data = new List<string>();
    }

    public class XAxis
    { 
        public string type = "category";
        public IList<string> data = new List<string>();
    }

    public class YAxis
    {
        public string name = "";
        public string type = "value";
        public bool splitArea = true;
        //public int min = int.MaxValue;
        //public int max = int.MinValue;
    }

    public abstract class CommonItem
    {
        public string name { get; set; }
        public string type { get; set; }
        
    }

    public class SeriesItem : CommonItem
    {
        private IList<string> _Data = new List<string>();
        public IList<string> data
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }


    public class CommonOption
    {
        public Title title = new Title();
        public Legend legend = new Legend();
        public IList<CommonItem> series = new List<CommonItem>();
    }

    public class ChartOption : CommonOption
    { 
        public XAxis xAxis = new XAxis();
        public YAxis yAxis = new YAxis();
    }



    public class PieLegend : Legend
    {
         public string orient {get; set;}
    }

    public class PieDataItem
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class PieItem : CommonItem
    {
        public string radius { get; set; }
        public IList<string> center = new List<string>();
        public IList<PieDataItem> data = new List<PieDataItem>();
    }

    public class PieOption : CommonOption
    { 
    }
}
