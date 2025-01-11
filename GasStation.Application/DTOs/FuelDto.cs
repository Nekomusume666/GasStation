namespace GasStation.Application.DTOs
{
    public class FuelDto
    {
        public int ID_Fuel { get; set; }
        public decimal PricePerLiter { get; set; }
        public int Quantity { get; set; }
        public int ID_GasStation { get; set; }
        public int ID_FuelType { get; set; }
    }
}
