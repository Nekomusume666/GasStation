namespace GasStation.Domain.Models
{
    public class News
    {
        public int ID_News { get; set; } // Уникальный идентификатор

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Внешний ключ для связи с Administrator
        public int ID_Administrator { get; set; }
        public Administrator Administrator { get; set; } // Навигационное свойство для Administrator
    }
}
