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
    public sealed partial class AjoutActiviter : Page
    {
        public AjoutActiviter()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var nom = NomTextBox.Text;
            var Type = TypeTextBox.Text;
            var CoutUnitaire = int.Parse(PrixOrganisationTextBox.Text);
            var CoutClients = int.Parse(PrixClientsTextBox.Text);

            ConnextionDb.GetInstance().AjoutActiviter(new Activiter { Nom = nom, Type = Type, CoutUnitaire = CoutUnitaire, CoutClient = CoutClients });

            Frame.Navigate(typeof(GestionActiviter));
        }
    }
}
