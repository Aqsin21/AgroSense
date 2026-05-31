namespace AgroSense.Domain.Entities
{
    public class Plant
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!; 

        public int PlantProfileId { get; set; }
        public PlantProfile PlantProfile { get; set; } = null!;

        public int AreaId { get; set; }
        public Area Area { get; set; } = null!;

        public DateTime PlantedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
