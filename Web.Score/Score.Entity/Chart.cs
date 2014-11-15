
namespace App.Score.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Legend
    {
        public string[] data;
    }

    public class XAxis
    {
        public string type;
        public string[] data;
    }

    public class SeriesItem
    {
        public string name;
        public string type;
        public string[] data;
    }

    public class ChartOption
    {
        public Legend legend;
        public XAxis xAxis;
        public SeriesItem[] series;
    }
}
