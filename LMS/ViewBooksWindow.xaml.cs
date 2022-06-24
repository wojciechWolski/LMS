using LMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace LMS
{
    /// <summary>
    /// Logika interakcji dla klasy ViewBooksWindow.xaml
    /// </summary>
    public partial class ViewBooksWindow : Window
    {
        readonly string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";

        public ViewBooksWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from bk in db.Books where tbBookToFind.Text == bk.Title select new { bk.Title, bk.Author, bk.Genre, bk.PublicationHouse };
                dgBookView.ItemsSource = query.ToList();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from bk in db.Books select new { bk.Id, bk.Title, bk.Author, bk.Genre, bk.PublicationHouse };
                dgBookView.ItemsSource = query.ToList();
            }
                
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from bk in db.Books select new { bk.Title, bk.Author, bk.Genre, bk.PublicationHouse };
                dgBookView.ItemsSource = query.ToList();
            }
        }
        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
            if (this.manageGrid.Visibility == Visibility.Visible)
            {
                this.manageGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.manageGrid.Visibility = Visibility.Visible;
            }
        }


 
    }
}
