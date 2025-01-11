using System.Transactions;

namespace GasStation.Domain.Models
{
    public class Client
    {
        public int ID_Client { get; set; } // Primary Key
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; } // Nullable
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? BonusPoints { get; set; } // Nullable

        // Связи
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
