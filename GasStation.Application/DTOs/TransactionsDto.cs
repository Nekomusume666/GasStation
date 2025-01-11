namespace GasStation.Application.DTOs
{
    public class TransactionsDto
    {
        public int ID_Transactions { get; set; }
        public int ID_Client { get; set; }
        public int ID_Fuel { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public int? BonusPoints { get; set; }
        public int ID_Pump { get; set; }
    }
}
