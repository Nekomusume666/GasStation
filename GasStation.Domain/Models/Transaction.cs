namespace GasStation.Domain.Models
{
    public class Transaction
    {
        public int ID_Transaction { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public int? BonusPoints { get; set; }

        // Связь с Client
        public int ID_Client { get; set; }
        public Client Client { get; set; }

        // Связь с Fuel
        public int ID_Fuel { get; set; }
        public Fuel Fuel { get; set; }

        // Связь с Pump
        public int ID_Pump { get; set; }
        public Pump Pump { get; set; }
    }
}
