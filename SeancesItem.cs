using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProjet
{
    public class SeancesItem
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get { return "Date : " + Date.ToString("MM/dd/yyyy"); } }
        public string Heure { get; set; }
        public string HeureString { get { return "Heure : " + Heure; } }
        public int PlaceDispo { get; set; }
        public int PlaceMax { get; set; }
        public string Place { get { return "Places restantes : " + PlaceDispo + "/" + PlaceMax; } }
        public string Nom { get; set; }

        public Visibility IsButtonVisible { get; set; } = Visibility.Collapsed;



    }
}
