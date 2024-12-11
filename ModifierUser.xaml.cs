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
    public sealed partial class ModifierUser : Page
    {
        string id = string.Empty;
        public ModifierUser()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string)
            {
               
                var user = ConnextionDb.GetInstance().GetUser(e.Parameter.ToString());
                if (user == null) { 
                    Frame.Navigate(typeof(GestionUtilisateur));
                    return;
                }
                id = user.Id;
                NomTextBox.Text = user.Nom;
                PrenomTextBox.Text = user.Prenom;
                AdresseTextBox.Text = user.Adresse;
                DateNaissancePicker.Date = user.DateNaissance;
            }
            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var nom = NomTextBox.Text;
            var prenom = PrenomTextBox.Text;
            var adresse = AdresseTextBox.Text;
            DateTime date = DateNaissancePicker.Date.DateTime;

            ConnextionDb.GetInstance().ModifierUser(new Utilisateur { Nom = nom, Prenom = prenom, Adresse = adresse, DateNaissance = date, Id = id });

            Frame.Navigate(typeof(GestionUtilisateur));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GestionUtilisateur));
        }
    }
}
