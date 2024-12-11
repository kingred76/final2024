using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace finalProjet
{
    public class Utilisateur
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public DateTime DateNaissance { get; set; }
        public string DateString { get { return "Date : " + DateNaissance.ToString("MM/dd/yyyy"); } }
        public int Age { get; set; }

        public override string ToString()
        {
            return Id + ";" + Nom + ";" + Adresse + ";" + DateString + ";" + Age;
        }
    }
}
