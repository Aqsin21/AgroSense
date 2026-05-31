namespace AgroSense.Domain.Entities
{
    public class PlantCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<PlantProfile> PlantProfiles { get; set; } = new List<PlantProfile>();
    }
}
