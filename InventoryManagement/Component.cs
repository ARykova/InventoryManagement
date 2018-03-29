using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class Component
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public int IntervalTime { get; set; }

        public double SupplyTime { get; set; }

        public double DelayTime { get; set; }

        public double SupplyCount { get; set; }

        public string Supplier { get; set; }
    }
}
