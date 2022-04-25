using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volitve2022.CosmosDB
{
    public class CosmosDbSettings
    {
        public const string CosmosDbSettingsSection = "CosmosDb";

        public string Endpoint { get; set; }
        public string MasterKey { get; set; }

        public string Database { get; set; }
        public string Container { get; set; }


        public string[] PreferredRegions { get; set; }
    }
}
