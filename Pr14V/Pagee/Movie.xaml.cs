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
using static Pr14V.Page.Sessions;

namespace Pr14V.Page
{
    // Имя класса должно быть СТРОГО MoviePage, как в x:Class твоего XAML
    public partial class MoviePage : System.Windows.Controls.Page
    {
        private Movies _selectedMovie;

        public MoviePage(Movies movie)
        {
            InitializeComponent();
            _selectedMovie = movie;

            // Заполняем данные фильма
            TxtTitle.Text = _selectedMovie.Title;
            TxtRating.Text = $"⭐ {_selectedMovie.Rating}";
            TxtDesc.Text = _selectedMovie.Description;

            // Загрузка фото
            if (!string.IsNullOrEmpty(_selectedMovie.PosterPath))
            {
                try
                {
                    ImgPoster.Source = new BitmapImage(new Uri(_selectedMovie.PosterPath, UriKind.RelativeOrAbsolute));
                }
                catch { /* Ошибка если путь неверный */ }
            }

            // Грузим сеансы именно для этого фильма
            LBoxScreenings.ItemsSource = App.Context.Screenings
                .Where(s => s.MovieId == _selectedMovie.MovieId).ToList();
        }

        private void LBoxScreenings_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (LBoxScreenings.SelectedItem is Screenings selectedScreening)
            {
                // Проверка авторизации (по заданию выбор места только после входа)
                if (App.CurrentUser == null)
                {
                    MessageBox.Show("Для выбора места необходимо войти в аккаунт!");
                    NavigationService.Navigate(new Reg());
                }
                else
                {
                    // Переход на страницу выбора мест (создадим её следующей)
                    NavigationService.Navigate(new Sessions(selectedScreening));
                }
            }
        }
    }
}
