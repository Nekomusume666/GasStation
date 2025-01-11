namespace GasStation.Domain.Models
{
    public class Fuel
    {
        public int ID_Fuel { get; set; }
        public decimal PricePerLiter { get; set; }
        public int Quantity { get; set; }

        // Связь с GasStation
        public int ID_GasStation { get; set; }
        public GasStation GasStation { get; set; } // Навигационное свойство

        // Связь с FuelType
        public int ID_FuelType { get; set; }
        public FuelType FuelType { get; set; } // Навигационное свойство
    }
}
