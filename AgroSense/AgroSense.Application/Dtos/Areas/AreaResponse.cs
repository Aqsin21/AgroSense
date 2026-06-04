using AgroSense.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSense.Application.Dtos.Areas
{
    public class AreaResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public AreaType AreaType { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public bool IsActive { get; set; }

        public string OwnerUserId { get; set; } = null!;
    }
}
