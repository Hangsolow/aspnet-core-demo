using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspnetcore.Demo.Options
{
    public class ApiOptions
    {
        public string Name { get; set; }

        public string Key { get; set; }

        public int Number { get; set; } = -1;
    }
}
