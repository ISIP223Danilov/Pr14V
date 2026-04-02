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
using System.Data.Entity;

namespace Pr14V.Pagee
{
    public partial class Profile : System.Windows.Controls.Page
    {
        public Profile()
        {
            InitializeComponent();
            
            // Заставляем страницу обновляться каждый раз, когда мы на нее заходим
            this.Loaded += (s, e) => LoadData();
        }

        private void LoadData()
        {
            if (App.CurrentUser != null)
            {
                TxtUserDisplay.Text = App.CurrentUser.Username;

                // Попробуем загрузить без сложных связей сначала, чтобы проверить количество
                var testList = App.Context.Tickets
                    .Where(t => t.UserId == App.CurrentUser.UserId)
                    .ToList();

                // ВЫВЕДИ ЭТО:
                MessageBox.Show($"В базе найдено билетов для вас: {testList.Count}");

                // Если билеты есть (больше 0), но на экране пусто — значит ошибка в именах (Screening или Screenings)
                var myTickets = App.Context.Tickets
                    .Include(t => t.Screenings.Movies)
                    .Include(t => t.Screenings.Halls)
                    .Where(t => t.UserId == App.CurrentUser.UserId)
                    .ToList();

                LBoxUserTickets.ItemsSource = myTickets;
            }
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            NavigationService.Navigate(new MainPage());
        }
    }

}
