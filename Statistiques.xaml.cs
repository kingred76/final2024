using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace finalProjet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Statistiques : Page
    {
        public Statistiques()
        {
            this.InitializeComponent();
            var totalUser = ConnextionDb.GetInstance().CountClients();
            var totalActiviter = ConnextionDb.GetInstance().CountActiviter();
            var moyenneAge = ConnextionDb.GetInstance().MoyenneAge();
            var placeDispo = ConnextionDb.GetInstance().CountPlace();
            var money = ConnextionDb.GetInstance().PrixMax();
            TotalUser.Text =   totalUser.ToString();
            TotalActiviter.Text =  totalActiviter.ToString();
            MoyenneAge.Text =  moyenneAge.ToString();
            PlaceDispo.Text = placeDispo.ToString();
            Money.Text = money.ToString() + "$";
            LoadStats();

        }

        public void LoadStats()
        {
            var activiter = ConnextionDb.GetInstance().GetAllActiviter();
            foreach(var a in activiter)
            {

          
                ParticipantsList.Items.Add($"{a.Nom}: {ConnextionDb.GetInstance().Countinscription(a.Nom)} ");
                NotesList.Items.Add($"{a.Nom} {ConnextionDb.GetInstance().Moyenne(a.Nom)}");
            }
            
        }

        public class ActiviterStat
        {
            public string Nom { get; set; }
            public string Moyenne {  get; set; }
            public string NbrParticipant { get; set; }

        }
    }


}
