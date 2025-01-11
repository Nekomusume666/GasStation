namespace GasStation.Domain.Models
{
    public class GasStation
    {
        public int ID_GasStation { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }

        public int? ID_Administrator { get; set; }
        public Administrator Administrator { get; set; }

        // Коллекция связей с Fuel
        public ICollection<Fuel> Fuels { get; set; }
    }
}
