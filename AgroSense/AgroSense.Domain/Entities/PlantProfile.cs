namespace AgroSense.Domain.Entities
{
    public class PlantProfile
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!; 

        public int PlantCategoryId { get; set; }
        public PlantCategory PlantCategory { get; set; } = null!;

        public decimal MinSoilMoisture { get; set; }
        public decimal MaxSoilMoisture { get; set; }

        public decimal MinAirHumidity { get; set; }
        public decimal MaxAirHumidity { get; set; }

        public decimal MinTemperature { get; set; }
        public decimal MaxTemperature { get; set; }

        public decimal DailyWaterNeedLiters { get; set; }

        public ICollection<Plant> Plants { get; set; } = new List<Plant>();
    }
}
