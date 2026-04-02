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
using static Pr14V.Page.MoviePage;
using Page = System.Windows.Controls.Page;

namespace Pr14V.Page
{
    public partial class MainPage : System.Windows.Controls.Page
    {
        // Подключаем базу из статического контекста App
        private CinemaDBEntities _db = App.Context;

        public MainPage()
        {
            InitializeComponent();
            UpdateData();
        }

        // ОДИН метод для всей логики
        private void UpdateData()
        {
            // ЗАЩИТА: Проверяем, созданы ли элементы управления
            if (LBoxMovies == null || TBoxSearch == null || ComboSort == null)
                return;

            // 1. Получаем список фильмов из БД
            var currentMovies = _db.Movies.ToList();

            // 2. ПОИСК: фильтруем по тексту в TBoxSearch
            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
            {
                currentMovies = currentMovies
                    .Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower()))
                    .ToList();
            }

            // 3. СОРТИРОВКА: проверяем выбранный индекс в ComboSort
            if (ComboSort.SelectedIndex == 1) // По названию
                currentMovies = currentMovies.OrderBy(p => p.Title).ToList();
            else if (ComboSort.SelectedIndex == 2) // По рейтингу
                currentMovies = currentMovies.OrderByDescending(p => p.Rating).ToList();

            // 4. ОТОБРАЖЕНИЕ: выводим итоговый список
            LBoxMovies.ItemsSource = currentMovies;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем: если в App.CurrentUser КТО-ТО ЕСТЬ (уже вошел или зарегился)
            if (App.CurrentUser != null)
            {
                // Идем сразу на страницу профиля
                NavigationService.Navigate(new Pagee.Profile());
            }
            else
            {
                // Если там пусто (null) — отправляем на регистрацию
                NavigationService.Navigate(new Reg());
            }
        }


        private void LBoxMovies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LBoxMovies.SelectedItem is Movies selectedMovie) // Проверь имя класса (Movies или Movie)
            {
                // Переходим на страницу фильма и ПЕРЕДАЕМ объект фильма
                NavigationService.Navigate(new MoviePage(selectedMovie));
            }
        }



    }
}
