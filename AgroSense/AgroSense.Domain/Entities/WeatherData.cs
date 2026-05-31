namespace AgroSense.Domain.Entities
{
    public class WeatherData
    {
        public int Id { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; } = null!;

        public decimal Temperature { get; set; }

        public decimal Humidity { get; set; }

        public decimal RainProbability { get; set; }

        public decimal? RainAmountMm { get; set; }

        public decimal? WindSpeed { get; set; }

        public DateTime ForecastDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
