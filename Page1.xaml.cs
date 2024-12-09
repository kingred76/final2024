using finalProjet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace finalProjet
{
    public sealed partial class Page1 : Page
    {
        public Page1()
        {
            this.InitializeComponent();
        }

        private void UserLogin_Click(object sender, RoutedEventArgs e)
        {
            string userId = UserIdInput.Text;
            var BD = ConnextionDb.GetInstance();
            var id = BD.UserLogIn(userId);
            if (id != null)
            {
                App.CurrentUserRole = "user";
                Frame.Navigate(typeof(Page2),id);
            }
            else
            {
                ShowError("ID  incorrect.");
            }
        }

        private void AdminLogin_Click(object sender, RoutedEventArgs e)
        {
            string adminId = AdminIdInput.Text;
            string adminPassword = AdminPasswordInput.Password;
            var BD = ConnextionDb.GetInstance();
            var id = BD.AdminLogIn(adminId, adminPassword);
            if (id !=null)
            {
                App.CurrentUserRole = "admin";
                Frame.Navigate(typeof(Page2), id);
            }
            else
            {
                ShowError("ID ou mot de passe incorrect.");
            }
        }

        private void ShowError(string message)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Erreur de Connexion",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot,
            };

            _ = errorDialog.ShowAsync();
        }
    }
}
