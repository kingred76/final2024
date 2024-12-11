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
using System.Xml.Linq;
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
    public sealed partial class Seances : Page
    {
        
        public ObservableCollection<SeancesItem> SeancesCollection { get; set; } = new ObservableCollection<SeancesItem>();
        public Seances()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string)
            {
               var Isreserve = ConnextionDb.GetInstance().UserActiviter(Page2.userid, e.Parameter.ToString());
               var date = ConnextionDb.GetInstance().GetAllSeance(e.Parameter.ToString());

                foreach (var item in date) {
                item.IsButtonVisible = Isreserve || App.CurrentUserRole=="admin" ? Visibility.Collapsed : Visibility.Visible;
                SeancesCollection.Add(item);
                }
            }
            base.OnNavigatedTo(e);
           
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer l'objet lié au bouton cliqué
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
               
                SeancesItem selectedSeance = clickedButton.DataContext as SeancesItem;

                if (selectedSeance != null)
                {
                   
                    var connexionDB = ConnextionDb.GetInstance();
                    var id = selectedSeance.Id;
                    var userID = Page2.userid;
                    connexionDB.RegisterToSeance(userID, id);
                  

               
                }
            }
            Frame.Navigate(typeof(Page3));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page3));
        }
    }


}
