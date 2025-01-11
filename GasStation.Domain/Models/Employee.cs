namespace GasStation.Domain.Models
{
    public class Employee
    {
        public int ID_Employee { get; set; } // Уникальный идентификатор
        public string LastName { get; set; } = string.Empty; // Фамилия
        public string FirstName { get; set; } = string.Empty; // Имя
        public string? MiddleName { get; set; } // Отчество (может быть null)
        public string Phone { get; set; } = string.Empty; // Телефон
        public string Email { get; set; } = string.Empty; // Электронная почта
        public int ID_GasStation { get; set; } // ID АЗС

        // Навигационное свойство для связи с GasStation
        public GasStation GasStation { get; set; } = null!;
    }
}
