
namespace App.Score.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
        public int min = 0;
    }

    public class SeriesItem
    {
        public string name;
        public string type;
        public IList<string> data = new List<string>();
    }

    public class ChartOption
    {
        public Legend legend = new Legend();
        public XAxis xAxis = new XAxis();
        public YAxis yAxis = new YAxis();
        public IList<SeriesItem> series = new List<SeriesItem>();
    }
}
