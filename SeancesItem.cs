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
    public string DateString { get { return Date.ToString("dd/MM/yyyy"); } }
    public string Heure { get; set; }
    public int PlaceDispo {  get; set; }
    public int PlaceMax { get; set; }
    public string Place { get { return "Places : " + PlaceDispo + "/" + PlaceMax; } }
    public string Nom {  get; set; }
    }
}
