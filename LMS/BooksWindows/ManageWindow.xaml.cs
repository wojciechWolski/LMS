using LMS.Models;
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
using System.Windows.Shapes;

namespace LMS
{
    /// <summary>
    /// Logika interakcji dla klasy ManageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {
        readonly string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
        int bookIdToManage = ViewBooksWindow.BookId;
        public ManageWindow()
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

        /// <summary>
        /// Wyświetlanie zarządzanej aktualnie książki w DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from bk in db.Books where bk.Id == bookIdToManage select new { bk.Id, bk.Title, bk.Author, bk.Genre, bk.PublicationHouse };
                dgCurrent.ItemsSource = query.ToList();
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbBName.Text = "";
            tbAuthor.Text = "";
            tbGenre.Text = "";
            tbPublish.Text = "";
        }

        /// <summary>
        /// Usuwanie aktualnie zarządzanej książki z bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        { 
            if (MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (LibdbContext db = new LibdbContext(connectionString))
                {
                    Book book = db.Books.Where(u => u.Id == bookIdToManage).First();
                    db.Remove(book);
                    db.SaveChanges();
                    MessageBox.Show("Item deleted successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    var query = from bk in db.Books where bk.Id == bookIdToManage select new { bk.Id, bk.Title, bk.Author, bk.Genre, bk.PublicationHouse };
                    dgCurrent.ItemsSource = query.ToList();
                }
            }

        }

        /// <summary>
        /// Edycja aktualnie zarządzanej książki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                if (MessageBox.Show("Are you sure you want to edit this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Book book = db.Books.Where(u => u.Id == bookIdToManage).First();
                    book.Title = tbBName.Text;
                    book.Author = tbAuthor.Text;
                    book.Genre = tbGenre.Text;
                    book.PublicationHouse = tbPublish.Text;
                    db.SaveChanges();
                    var query = from bk in db.Books where bk.Id == bookIdToManage select new { bk.Id, bk.Title, bk.Author, bk.Genre, bk.PublicationHouse };
                    dgCurrent.ItemsSource = query.ToList();
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ViewBooksWindow vbw = new ViewBooksWindow();
            vbw.Show();
        }
    }
    }
