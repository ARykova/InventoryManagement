using System.ComponentModel;

namespace InventoryManagement
{
    public class Component
    {
        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Шт./изд.")]
        public int Count { get; set; }

        [DisplayName("Цена")]
        public double Price { get; set; }

        [DisplayName("Время между поставками")]
        public int IntervalTime { get; set; }

        [DisplayName("Время поставки")]
        public double SupplyTime { get; set; }

        [DisplayName("Задержка")]
        public double DelayTime { get; set; }

        [DisplayName("Партия")]
        public double SupplyCount { get; set; }

        [DisplayName("Поставщик")]
        public string Supplier { get; set; }
    }
}
