using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Auth.Dto
{
    public class UsersDto
    {
        public Guid userId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
