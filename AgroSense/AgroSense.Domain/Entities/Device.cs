using AgroSense.Domain.Enums;
namespace AgroSense.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DeviceType DeviceType { get; set; }

        public string? SerialNumber { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public bool CurrentState { get; set; }
    }
}
