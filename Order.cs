using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderList.Models
{
    public class Order
    {
        public int OrderId;
        public int CustomerId;
        public string FirtsName;
        public string CustomerName;
        public string ItemName;
        public string ItemInformation;
        public int CostOfItem;
    }
}