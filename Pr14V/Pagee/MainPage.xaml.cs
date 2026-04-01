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
using Page = System.Windows.Controls.Page;

namespace Pr14V.Page
{
    public partial class MainPage : System.Windows.Controls.Page
    {
        
        private CinemaDBEntities _db = App.Context;

        public MainPage()
        {
            InitializeComponent();
            UpdateData(); 
        }

        
        private void UpdateData()
        {
         
            var currentMovies = _db.Movies.ToList();

          
            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
            {
                currentMovies = currentMovies.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            }

         
            if (ComboSort.SelectedIndex == 1)
                currentMovies = currentMovies.OrderBy(p => p.Title).ToList();
            else if (ComboSort.SelectedIndex == 2) 
                currentMovies = currentMovies.OrderByDescending(p => p.Rating).ToList();

            
            LBoxMovies.ItemsSource = currentMovies;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e) => UpdateData();

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateData();

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            // Здесь будет переход: NavigationService.Navigate(new Reg());
        }

        private void LBoxMovies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Здесь будет переход на страницу выбранного фильма
        }
    }
}
