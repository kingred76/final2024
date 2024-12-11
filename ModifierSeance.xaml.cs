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
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace finalProjet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModifierSeance : Page
    {
        public int id = 0;
        public ModifierSeance()
        {
            this.InitializeComponent();

            ChargerActivites();
        }

        private void ChargerActivites()
        {

            List<Activiter> activites = ConnextionDb.GetInstance().GetAllActiviter();

            NomComboBox.ItemsSource = activites.Select(a => a.Nom).ToList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string)
            {
                id = int.Parse(e.Parameter.ToString());
                var Seance = ConnextionDb.GetInstance().GetSeance(int.Parse(e.Parameter.ToString()));
                if (Seance == null)
                {
                    Frame.Navigate(typeof(GestionSeance));
                    return;
                }
                NomComboBox.SelectedItem = Seance.Nom;
                DatePicker.Date = Seance.Date;
                HeureTextBox.Text = Seance.Heure;
                PlaceDispoTextBox.Text = Seance.PlaceDispo.ToString();
                PlaceMaxTextBox.Text = Seance.PlaceMax.ToString();
            }
            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var nom = NomComboBox.SelectedItem as string;
            DateTime date = DatePicker.Date.DateTime;
            var heure = HeureTextBox.Text;
            int placeDispo = 0;
            int placeMax = 0;
            try
            {
                 placeDispo = int.Parse(PlaceDispoTextBox.Text);
                 placeMax = int.Parse(PlaceMaxTextBox.Text);
            }
            catch { }
            


            ConnextionDb.GetInstance().ModifierSeance(new SeancesItem { Nom = nom, Heure = heure, Date = date, PlaceDispo = placeDispo, PlaceMax = placeMax, Id = id });

            Frame.Navigate(typeof(GestionSeance));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GestionSeance));
        }
    }
}
