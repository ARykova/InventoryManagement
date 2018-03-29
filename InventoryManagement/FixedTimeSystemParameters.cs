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
        
        public string ComponentName { get { return CurrentComponent.Name; } }

        public double Demand { get; set; }

        public int IntervalTime { get { return CurrentComponent.IntervalTime; } }

        public double SupplyTime { get { return CurrentComponent.SupplyTime; } }

        public double DelayTime { get { return CurrentComponent.DelayTime; } }

        public double DailyConsumption { get { return Demand / 240; } }

        public double SupplyTimeConsumption { get { return DailyConsumption * SupplyTime; } }

        public double MaxConsumption { get { return (SupplyTime + DelayTime) * DailyConsumption; } }

        public double GuaranteeReserve { get { return MaxConsumption - SupplyTimeConsumption; } }

        public double MaxReserve { get { return GuaranteeReserve + IntervalTime * DailyConsumption; } }

        public double OrderSize { get { return MaxReserve - MaxConsumption + SupplyTimeConsumption; } }
    }
}
