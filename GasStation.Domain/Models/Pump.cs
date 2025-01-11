namespace GasStation.Domain.Models
{
    public class Pump
    {
        public int ID_Pump { get; set; } // Уникальный идентификатор насоса

        public int ID_GasStation { get; set; } // Внешний ключ для АЗС
        public GasStation GasStation { get; set; } // Навигационное свойство для связи с GasStation

        public int ID_FuelType { get; set; } // Внешний ключ для типа топлива
        public FuelType FuelType { get; set; } // Навигационное свойство для связи с FuelType
    }
}
