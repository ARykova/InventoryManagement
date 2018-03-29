using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class FixedSizeSystemParameters
    {
        public FixedSizeSystemParameters(Component comp, double dem)
        {
            CurrentComponent = comp;
            Demand = dem;
        }

        private Component CurrentComponent { get; set; }

        public string ComponentName
        {
            get
            {
                return CurrentComponent.Name;
            }
        }

        public double Demand { get; set; }

        public double OptimalOrderSize
        {
            get
            {
                double A = (Demand / CurrentComponent.SupplyCount) * (CurrentComponent.Price * 0.25);
                double I = CurrentComponent.Price * 0.05;
                return Math.Sqrt((2 * A * Demand) / I);
            }
        }

        public double SupplyTime
        {
            get
            {
                return CurrentComponent.SupplyTime;
            }
        }

        public double DelayTime
        {
            get
            {
                return CurrentComponent.DelayTime;
            }
        }

        public double DailyConsumption
        {
            get
            {
                return Demand / 240;
            }
        }

        public int ConsumptionTime
        {
            get
            {
                return Convert.ToInt32(OptimalOrderSize / DailyConsumption);
            }
        }

        public double SupplyTimeConsumption
        {
            get
            {
                return DailyConsumption * SupplyTime;
            }
        }

        public double MaxConsumption
        {
            get
            {
                return (SupplyTime + DelayTime) * DailyConsumption;
            }
        }

        public double GuaranteeReserve
        {
            get
            {
                return MaxConsumption - SupplyTimeConsumption;
            }
        }

        public double ThresholdLevel
        {
            get
            {
                return GuaranteeReserve - SupplyTimeConsumption;
            }
        }

        public double MaxReserve
        {
            get
            {
                return GuaranteeReserve + OptimalOrderSize;
            }
        }

        public double ConsumptionToThresholdTime
        {
            get
            {
                return (MaxReserve - ThresholdLevel) / DailyConsumption;
            }
        }
    }
}
