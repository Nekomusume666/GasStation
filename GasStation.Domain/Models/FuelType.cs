namespace GasStation.Domain.Models
{
    public class FuelType
    {
        public int ID_FuelType { get; set; }
        public string Type { get; set; }

        // Связь с Fuel может быть добавлена, если требуется
        public ICollection<Fuel> Fuels { get; set; }
    }
}
