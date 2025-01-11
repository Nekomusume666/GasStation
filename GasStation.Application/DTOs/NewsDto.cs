namespace GasStation.Application.DTOs
{
    public class NewsDto
    {
        public int ID_News { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ID_Administrator { get; set; }
    }
}
