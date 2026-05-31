using AgroSense.Domain.Enums;
namespace AgroSense.Domain.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public AreaType AreaType { get; set; }
        public string OwnerUserId { get; set; } = null!;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Plant> Plants { get; set; } = new List<Plant>();
        public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
        public ICollection<Device> Devices { get; set; } = new List<Device>();
        public ICollection<WeatherData> WeatherData { get; set; } = new List<WeatherData>();
    }

}
