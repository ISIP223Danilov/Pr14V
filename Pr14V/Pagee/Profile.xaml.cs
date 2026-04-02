using Pr14V.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pr14V.Pagee
{
   
    public partial class Profile : System.Windows.Controls.Page
    {
        public Profile()
        {
            InitializeComponent();
            if (App.CurrentUser != null)
            {
                
                TxtFullName.Text = App.CurrentUser.FullName;
                TxtEmail.Text = App.CurrentUser.Email;
                TxtUsername.Text = $"Логин: {App.CurrentUser.Username}";

               
                LBoxUserTickets.ItemsSource = App.Context.Tickets
                    .Where(t => t.UserId == App.CurrentUser.UserId)
                    .ToList();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null; // Очищаем сессию
            NavigationService.Navigate(new MainPage());
        }
    }
    
}
