using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Domain.Enums
{
    public enum UserRolesEnum
    {
        [Description("Reader")]
        Reader = 0,

        [Description("Writer")]
        Writer = 1
    }
}
