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

namespace Pr14V.Page
{
    public partial class Checkout : System.Windows.Controls.Page
    {
        // Переменные, которые будут хранить данные на уровне всего класса
        private Pr14V.Models.Session _session;
        private Pr14V.Models.Seat _seat;

        // Оставляем один основной конструктор, который принимает данные
        public Checkout(Pr14V.Models.Session selectedSession, Pr14V.Models.Seat selectedSeat)
        {
            InitializeComponent();

            // Сохраняем переданные данные в наши переменные выше
            _session = selectedSession;
            _seat = selectedSeat;

            // (Опционально) Здесь можно вывести данные на экран, если есть TextBlock-и
            // TxtMovieName.Text = _session.Movies.Title; 
            // TxtSeatInfo.Text = $"Место: {_seat.Number}";
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // В методе BtnConfirm_Click в Checkout.xaml.cs
            if (App.CurrentUser.UserId == 0)
            {
                MessageBox.Show("ОШИБКА: Программа потеряла ваш ID! Билет не будет виден в профиле.");
                return;
            }

            if (App.CurrentUser == null)
            {
                MessageBox.Show("Ошибка: войдите в аккаунт!");
                return;
            }

            try
            {
                // ПРОВЕРКА: какой ID мы отправляем в базу?
                int currentUserId = App.CurrentUser.UserId;
                if (currentUserId == 0)
                {
                    MessageBox.Show("Критическая ошибка: ID пользователя равен 0. Билет не привяжется!");
                    return;
                }

                
                var newTicket = new Pr14V.Tickets
                {
                    ScreeningId = _session.SessionID,
                    UserId = App.CurrentUser.UserId, // УБЕДИСЬ, ЧТО ЭТО ПОЛЕ НЕ 0!
                    SeatNumber = _seat.SeatID,
                    PurchasedAt = DateTime.Now
                };
                App.Context.Tickets.Add(newTicket);
                App.Context.SaveChanges();
                // Добавь это для теста:
                MessageBox.Show($"Билет сохранен для пользователя с ID: {App.CurrentUser.UserId}");

                App.Context.Tickets.Add(newTicket);
                int result = App.Context.SaveChanges(); // Сохраняем и получаем кол-во измененных строк

                if (result > 0)
                {
                    MessageBox.Show($"Успех! Билет №{newTicket.TicketId} сохранен для пользователя ID: {currentUserId}");
                    NavigationService.Navigate(new MainPage());
                }
                else
                {
                    MessageBox.Show("База данных не сохранила билет по неизвестной причине.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

    }
}
