using finalProjet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace finalProjet
{
    public sealed partial class Page2 : Page
    {
        public static string userid;
        public Page2()
        {
            this.InitializeComponent();
            ConfigureMenue();
            
        }
       
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is string)
            {
                userid = (string)e.Parameter;
            }
            base.OnNavigatedTo(e);
            menu.SelectedItem = "Page3";
            switch (App.CurrentUserRole)
            {
                case "user": 
                    name.Text = ConnextionDb.GetInstance().UserName(userid);
                    break;
                case "admin":
                    name.Text = ConnextionDb.GetInstance().AdminName(userid);
                    break;

            }
        }

        private void deconnexion(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page1));
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
           
            var navigationView = sender as NavigationView;
            if (navigationView != null && navigationView.MenuItems.Count > 0)
            {
                navigationView.SelectedItem = navigationView.MenuItems[0];
            }
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                switch (selectedItem.Tag)
                {
                    case "Page3":
                        ContentFrame.Navigate(typeof(Page3));
                        break;
                    case "Page4":
                        ContentFrame.Navigate(typeof(Page4));
                        break;
                    case "Page5":
                        ContentFrame.Navigate(typeof(Page5));
                        break;
                    case "Page1":
                        Frame.Navigate(typeof(Page1));
                        break;
                }
            }
        }

        private void ConfigureMenue()
        {
            adminbut.Visibility = App.CurrentUserRole == "admin" ? Visibility.Visible : Visibility.Collapsed; 
            
        }
        
        private void NavigateToPage3(object sender, RoutedEventArgs e) => ContentFrame.Navigate(typeof(Page3));
        private void NavigateToPage4(object sender, RoutedEventArgs e) => ContentFrame.Navigate(typeof(Page4));
        private void NavigateToPage5(object sender, RoutedEventArgs e) => ContentFrame.Navigate(typeof(Page5));
    }
}
