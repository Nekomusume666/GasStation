namespace GasStation.Domain.Models
{
    public class Supply
    {
        public int ID_Supply { get; set; } // Уникальный идентификатор

        // Внешние ключи для связи с другими сущностями
        public int ID_GasStation { get; set; }
        public GasStation GasStation { get; set; } // Навигационное свойство для GasStation

        public int ID_Employee { get; set; }
        public Employee Employee { get; set; } // Навигационное свойство для Employee

        public int ID_Fuel { get; set; }
        public Fuel Fuel { get; set; } // Навигационное свойство для Fuel

        // Другие свойства
        public DateTime SupplyDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
