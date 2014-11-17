
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
    }
    public class Legend
    {
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

    public class SeriesItem
    {
        public string name;
        public string type;
        public IList<string> data = new List<string>();
    }

    public class ChartOption
    {
        public Title title = new Title();
        public Legend legend = new Legend();
        public XAxis xAxis = new XAxis();
        public YAxis yAxis = new YAxis();
        public IList<SeriesItem> series = new List<SeriesItem>();
    }
}
