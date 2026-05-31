namespace AgroSense.Domain.Entities
{
    public class SensorReading
    {
        public int Id { get; set; }

        public int SensorId { get; set; }
        public Sensor Sensor { get; set; } = null!;

        public decimal Value { get; set; }

        public DateTime ReadAt { get; set; }
    }
}
