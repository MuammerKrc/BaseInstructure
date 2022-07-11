using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Configurations
{
    public class ClientConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public List<string> Audience { get; set; }
    }
}
