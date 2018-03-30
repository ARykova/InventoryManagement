using System;
using System.ComponentModel;

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

        [DisplayName("Деталь")]
        public string ComponentName
        {
            get
            {
                return CurrentComponent.Name;
            }
        }

        [DisplayName("Потребность")]
        public double Demand { get; set; }

        [DisplayName("Размер заказа")]
        public double OptimalOrderSize
        {
            get
            {
                double A = (Demand / CurrentComponent.SupplyCount) * (CurrentComponent.Price * 0.25);
                double I = CurrentComponent.Price * 0.05;
                return Math.Sqrt((2 * A * Demand) / I);
            }
        }

        [DisplayName("Время поставки")]
        public double SupplyTime
        {
            get
            {
                return CurrentComponent.SupplyTime;
            }
        }

        [DisplayName("Задержка")]
        public double DelayTime
        {
            get
            {
                return CurrentComponent.DelayTime;
            }
        }

        [DisplayName("Дневное потребление")]
        public double DailyConsumption
        {
            get
            {
                return Demand / 240;
            }
        }

        [DisplayName("Время расходования")]
        public int ConsumptionTime
        {
            get
            {
                return Convert.ToInt32(OptimalOrderSize / DailyConsumption);
            }
        }

        [DisplayName("Потребление за время поставки")]
        public double SupplyTimeConsumption
        {
            get
            {
                return DailyConsumption * SupplyTime;
            }
        }

        [DisplayName("Макс потребление")]
        public double MaxConsumption
        {
            get
            {
                return (SupplyTime + DelayTime) * DailyConsumption;
            }
        }

        [DisplayName("Гарантийный запас")]
        public double GuaranteeReserve
        {
            get
            {
                return MaxConsumption - SupplyTimeConsumption;
            }
        }

        [DisplayName("Порог запаса")]
        public double ThresholdLevel
        {
            get
            {
                return GuaranteeReserve - SupplyTimeConsumption;
            }
        }

        [DisplayName("Макс запас")]
        public double MaxReserve
        {
            get
            {
                return GuaranteeReserve + OptimalOrderSize;
            }
        }

        [DisplayName("Срок расходования до порога")]
        public double ConsumptionToThresholdTime
        {
            get
            {
                return (MaxReserve - ThresholdLevel) / DailyConsumption;
            }
        }
    }
}
