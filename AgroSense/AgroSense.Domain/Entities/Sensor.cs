using AgroSense.Domain.Enums;
namespace AgroSense.Domain.Entities
{
    public class Sensor
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public SensorType SensorType { get; set; }

        public string? SerialNumber { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();
    }
}
