using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSense.Application.Dtos.Auth
{
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;
    }
}
