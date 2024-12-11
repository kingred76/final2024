using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class GestionActiviter : Page
    {
        public ObservableCollection<Activiter> ActiviterCollection { get; set; } = new ObservableCollection<Activiter>();

        public GestionActiviter()
        {
            this.InitializeComponent();



        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ActiviterListView.ItemsSource = ActiviterCollection;
            var activiters = ConnextionDb.GetInstance().GetAllActiviter();
            foreach (var produit in activiters)
            {
                ActiviterCollection.Add(produit);
            }



            // Liaison des données
            this.DataContext = this;

            base.OnNavigatedTo(e);
        }

        private void ActiviterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActiviterListView.SelectedItem is Activiter a)
            {
                Frame.Navigate(typeof(Seances), a.Nom);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {

                Activiter selectedActiviter = clickedButton.DataContext as Activiter;

                if (selectedActiviter != null)
                {

                    var connexionDB = ConnextionDb.GetInstance();
                    var nom = selectedActiviter.Nom;
                    connexionDB.DeleteActiviter(nom);



                }
            }
            Frame.Navigate(typeof(GestionActiviter));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjoutActiviter));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {

                Activiter selectedActiviter = clickedButton.DataContext as Activiter;

                if (selectedActiviter != null)
                {

                    Frame.Navigate(typeof(ModifierActiviter), selectedActiviter.Nom);




                }
            }
        }
    }
}
