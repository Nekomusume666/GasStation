namespace GasStation.Application.DTOs
{
    public class SupplyDto
    {
        public int ID_Supply { get; set; }
        public int ID_GasStation { get; set; }
        public DateTime SupplyDate { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public int ID_Employee { get; set; }
        public int ID_Fuel { get; set; }
    }
}
