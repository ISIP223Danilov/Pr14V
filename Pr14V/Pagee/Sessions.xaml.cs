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
    // Класс должен называться Sessions (как в x:Class твоего XAML)
    public partial class Sessions : System.Windows.Controls.Page
    {
        private Screenings _screening;
        private int? _selectedSeat = null;

        public Sessions(Screenings screening)
        {
            InitializeComponent();
            _screening = screening;

            // Заполняем инфо (проверь имена полей Halls и Name в своей модели)
            TxtInfo.Text = $"Зал: {screening.Halls.Name} | Мест в зале: {screening.Halls.SeatsCount}";

            GenerateSeats();
        }

        private void GenerateSeats()
        {
            // Список уже занятых мест на этот сеанс
            var occupiedSeats = App.Context.Tickets
                .Where(t => t.ScreeningId == _screening.ScreeningId)
                .Select(t => t.SeatNumber).ToList();

            for (int i = 1; i <= _screening.Halls.SeatsCount; i++)
            {
                Button btn = new Button
                {
                    Content = i,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(5) // ОШИБКА CS0029 ИСПРАВЛЕНА: используем Thickness вместо int
                };

                if (occupiedSeats.Contains(i))
                {
                    btn.Background = Brushes.Red; // Занято
                    btn.IsEnabled = false;
                }
                else
                {
                    btn.Click += (s, e) =>
                    {
                        _selectedSeat = int.Parse((s as Button).Content.ToString());
                        MessageBox.Show($"Выбрано место: {_selectedSeat}");
                    };
                }
                GridSeats.Children.Add(btn);
            }
        }

        // ОШИБКА CS1061 ИСПРАВЛЕНА: Метод теперь существует
        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSeat == null)
            {
                MessageBox.Show("Сначала выберите место на схеме!");
                return;
            }

            // ОШИБКА CS0246: Если CheckoutPage еще нет, создай её или пока закомментируй переход
            // NavigationService.Navigate(new CheckoutPage(_screening, (int)_selectedSeat));

            // Пока можем просто выводить результат для проверки:
            MessageBox.Show($"Переходим к оформлению: Сеанс {_screening.ScreeningId}, Место {_selectedSeat}");
        }
    }
}
