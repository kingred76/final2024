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
    public sealed partial class ModifierActiviter : Page
    {
        public ModifierActiviter()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string)
            {

                var activiter = ConnextionDb.GetInstance().GetActiviter(e.Parameter.ToString());
                if (activiter == null)
                {
                    Frame.Navigate(typeof(GestionActiviter));
                    return;
                }
                NomTextBox.Text = activiter.Nom;
                TypeTextBox.Text = activiter.Type;
                PrixOrganisationTextBox.Text = activiter.CoutUnitaire.ToString();
                PrixClientsTextBox.Text = activiter.CoutClient.ToString();
            }
            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var nom = NomTextBox.Text;
            var type = TypeTextBox.Text;
            var prixOrganisation = float.Parse(PrixOrganisationTextBox.Text);
            var prixClient = float.Parse(PrixClientsTextBox.Text);


            ConnextionDb.GetInstance().ModifierActiviter(new Activiter { Nom = nom, Type = type, CoutUnitaire = prixOrganisation, CoutClient = prixClient });

            Frame.Navigate(typeof(GestionActiviter));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GestionActiviter));
        }
    }
}
