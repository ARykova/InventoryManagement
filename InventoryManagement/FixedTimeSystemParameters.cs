using System.ComponentModel;

namespace InventoryManagement
{
    public class FixedTimeSystemParameters
    {
        public FixedTimeSystemParameters(Component comp, double dem)
        {
            CurrentComponent = comp;
            Demand = dem;
        }

        private Component CurrentComponent { get; set; }

        [DisplayName("Деталь")]
        public string ComponentName { get { return CurrentComponent.Name; } }

        [DisplayName("Потребность")]
        public double Demand { get; set; }

        [DisplayName("Интервал между заказами")]
        public int IntervalTime { get { return CurrentComponent.IntervalTime; } }

        [DisplayName("Время поставки")]
        public double SupplyTime { get { return CurrentComponent.SupplyTime; } }

        [DisplayName("Задержка")]
        public double DelayTime { get { return CurrentComponent.DelayTime; } }

        [DisplayName("Дневное потребление")]
        public double DailyConsumption { get { return Demand / 240; } }

        [DisplayName("Потребление за время поставки")]
        public double SupplyTimeConsumption { get { return DailyConsumption * SupplyTime; } }

        [DisplayName("Макс потребление")]
        public double MaxConsumption { get { return (SupplyTime + DelayTime) * DailyConsumption; } }

        [DisplayName("Гарантийный запас")]
        public double GuaranteeReserve { get { return MaxConsumption - SupplyTimeConsumption; } }

        [DisplayName("Максимальный запас")]
        public double MaxReserve { get { return GuaranteeReserve + IntervalTime * DailyConsumption; } }

        [DisplayName("Размер заказа")]
        public double OrderSize { get { return MaxReserve - MaxConsumption + SupplyTimeConsumption; } }
    }
}
