using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Common
{
    public  class Delivery
    {


        public static List<int> DeliveryList(int id , int modevalue )
        {


            List<int> idlist = new List<int>();

            while ( id > 0 )
            {
                if (id >= modevalue)
                {
                    idlist.Add(modevalue);
                }
                id = id % modevalue;
                modevalue = modevalue / 2 ;
            }
           return idlist;
        }

    }
}
