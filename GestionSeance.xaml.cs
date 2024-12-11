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



namespace finalProjet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GestionSeance : Page
    {
        public ObservableCollection<SeancesItem> SeanceCollection { get; set; } = new ObservableCollection<SeancesItem>();

        
        public GestionSeance()
        {
            this.InitializeComponent();

            var data = ConnextionDb.GetInstance().GetAllSeance();

            foreach (var seance in data)
            {
                SeanceCollection.Add(seance);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page5));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {

                SeancesItem selectedSeance = clickedButton.DataContext as SeancesItem;

                if (selectedSeance != null)
                {

                    var connexionDB = ConnextionDb.GetInstance();
                    var Id = selectedSeance.Id;
                    connexionDB.DeleteSeance(Id);



                }
            }
            Frame.Navigate(typeof(GestionSeance));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjoutSeance));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {

                SeancesItem selectedSeanceItem = clickedButton.DataContext as SeancesItem;

                if (selectedSeanceItem != null)
                {

                    Frame.Navigate(typeof(ModifierSeance), selectedSeanceItem.Id.ToString());




                }
            }
        }
    }
}
