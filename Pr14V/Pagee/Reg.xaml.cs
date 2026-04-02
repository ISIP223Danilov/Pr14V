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
using Pr14V; 


namespace Pr14V.Page
{
    
    public partial class Reg : System.Windows.Controls.Page
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void BtnEntry_Click(object sender, RoutedEventArgs e)
        {
            var user = App.Context.Users.FirstOrDefault(u => u.Username == TBoxLogin.Text && u.PasswordHash == PBoxPass.Password);

            if (user != null)
            {
                App.CurrentUser = user; // Сохраняем пользователя
                MessageBox.Show("Успешный вход!");

                // Переходим на главную, чтобы купить билет
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Неверные данные!");
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            // Простая регистрация
            if (string.IsNullOrWhiteSpace(TBoxLogin.Text) || string.IsNullOrWhiteSpace(PBoxPass.Password))
            {
                MessageBox.Show("Заполните поля!");
                return;
            }

            var newUser = new Users
            {
                Username = TBoxLogin.Text,
                PasswordHash = PBoxPass.Password,
                CreatedAt = DateTime.Now
            };

            App.Context.Users.Add(newUser);
            App.Context.SaveChanges();

            App.CurrentUser = newUser; // Запоминаем нового пользователя в системе

            MessageBox.Show("Регистрация успешна!");
            NavigationService.Navigate(new MainPage()); // Сразу отправляем на главную
        }
        // Добавь это в файл Reg.xaml.cs
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack(); // Возвращаемся на предыдущую страницу (MainPage)
            }
        }


    }

}
