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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace finalProjet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GestionUtilisateur : Page
    {
        public ObservableCollection<Utilisateur> UtilisateurCollection { get; set; } = new ObservableCollection<Utilisateur>();
        public GestionUtilisateur()
        {
            this.InitializeComponent();
            var data = ConnextionDb.GetInstance().GetAllUser();
            foreach (var user in data)
            {
                UtilisateurCollection.Add(user);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {

                Utilisateur selectedUser = clickedButton.DataContext as Utilisateur;

                if (selectedUser != null)
                {

                    var connexionDB = ConnextionDb.GetInstance();
                    var id = selectedUser.Id;
                    connexionDB.DeleteUser(id);



                }
            }
            Frame.Navigate(typeof(GestionUtilisateur));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjoutClients));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {

                Utilisateur selectedUser = clickedButton.DataContext as Utilisateur;

                if (selectedUser != null)
                {

                   Frame.Navigate(typeof(ModifierUser), selectedUser.Id);
                   



                }
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page5));
        }
    }
}
