namespace GasStation.Domain.Models
{
    public class Administrator
    {
        public int ID_Administrator { get; set; } // Уникальный идентификатор
        public string LastName { get; set; } = string.Empty; // Фамилия
        public string FirstName { get; set; } = string.Empty; // Имя
        public string? MiddleName { get; set; } // Отчество (может быть null)
        public string Phone { get; set; } = string.Empty; // Телефон
        public string Email { get; set; } = string.Empty; // Электронная почта
        public string Login { get; set; } = string.Empty; // Логин
        public string Password { get; set; } = string.Empty; // Пароль
    }
}
