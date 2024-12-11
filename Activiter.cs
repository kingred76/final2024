using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProjet
{
    public class Activiter
    {
        public string Nom { get; set; }
        public string Type { get; set; }
        public float CoutUnitaire { get; set; }
        public float CoutClient { get; set; }

        public override string ToString()
        {
            return Nom + ";" + Type + ";" + CoutUnitaire + ";" + CoutClient;
        }
    }
}
