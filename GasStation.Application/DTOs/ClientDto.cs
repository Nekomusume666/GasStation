namespace GasStation.Application.DTOs
{
    public class ClientDto
    {
        public int ID_Client { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? BonusPoints { get; set; }
    }
}
