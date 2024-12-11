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



namespace finalProjet
{
    
    public sealed partial class AjoutSeance : Page
    {
        public AjoutSeance()
        {
            this.InitializeComponent();

            ChargerActivites();
        }

        private void ChargerActivites()
        {
           
            List<Activiter> activites = ConnextionDb.GetInstance().GetAllActiviter();

            NomComboBox.ItemsSource = activites.Select(a => a.Nom).ToList();
        }

        private void NomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            string nomActivite = NomComboBox.SelectedItem as string;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Date = DatePicker.Date.DateTime;
            var Heure = HeureTextBox.Text;
            var PlaceDispo = int.Parse(PlaceDispoTextBox.Text);
            var PlaceMax = int.Parse(PlaceMaxTextBox.Text);
            var Nom = NomComboBox.SelectedItem as string;

            ConnextionDb.GetInstance().AjoutSeance(new SeancesItem { Date = Date, Heure = Heure, PlaceDispo = PlaceDispo, PlaceMax = PlaceMax, Nom = Nom });

            Frame.Navigate(typeof(GestionSeance));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GestionSeance));
        }
    }
}
