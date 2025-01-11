namespace GasStation.Application.DTOs
{
    public class EmployeeDto
    {
        public int ID_Employee { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int ID_GasStation { get; set; }
    }
}
