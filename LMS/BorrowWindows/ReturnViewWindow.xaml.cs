using LMS.BorrowWindows;
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
    /// Logika interakcji dla klasy ReturnViewWindow.xaml
    /// </summary>
    public partial class ReturnViewWindow : Window
    {
        readonly string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
        public ReturnViewWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from ln in db.Borrows select new { ln.Id, ln.StudentId, ln.StudentEnroll, ln.BookId, ln.BookTitle, ln.BookLend, ln.BookReturn };
                var view = query.OrderByDescending(x => x.BookLend);
                dgBorrowView.ItemsSource = query.ToList();
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from ln in db.Borrows select new { ln.Id, ln.StudentId, ln.StudentEnroll, ln.BookId, ln.BookTitle, ln.BookLend, ln.BookReturn };
                var view = query.OrderByDescending(x => x.BookLend);
                dgBorrowView.ItemsSource = view.ToList();
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ReturnWindow rw = new ReturnWindow();
            rw.Show();
        }

        private void btnRtnNull_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from ln in db.Borrows where ln.BookReturn == null select new { ln.Id, ln.StudentId, ln.StudentEnroll, ln.BookId, ln.BookTitle, ln.BookLend };
                var view = query.OrderByDescending(x => x.BookLend);
                dgBorrowView.ItemsSource = view.ToList();
            }
        }
        /// <summary>
        /// Metoda wyszukiwania rekordów z bazy po tytule bądź numerze albumu (jeśli dane z textboxa można przekonwertować na int, znaczy że to numer albumu, jeśli nie, tzn że to nazwisko)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbEnTitFind.Text, out int enrollNmb))
            {
                using (LibdbContext db = new LibdbContext(connectionString))
                {
                    var q = from std in db.Students where std.EnrollmentNumber == tbEnTitFind.Text select std;
                    if (q.Count() > 0)
                    {
                        var query = from ln2 in db.Borrows where ln2.StudentEnroll == enrollNmb select new { ln2.Id, ln2.StudentId, ln2.StudentEnroll, ln2.BookId, ln2.BookTitle, ln2.BookLend, ln2.BookReturn };
                        var view = query.OrderByDescending(x => x.BookLend);
                        dgBorrowView.ItemsSource = view.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Wrong enrollment number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                using (LibdbContext db = new LibdbContext(connectionString))
                {
                    var q = from bk in db.Books where bk.Title == tbEnTitFind.Text select bk;
                    if(q.Count() > 0)
                    {
                        var query = from ln2 in db.Borrows where ln2.BookTitle == tbEnTitFind.Text select new { ln2.Id, ln2.StudentId, ln2.StudentEnroll, ln2.BookId, ln2.BookTitle, ln2.BookLend, ln2.BookReturn };
                        var view = query.OrderByDescending(x => x.BookLend);
                        dgBorrowView.ItemsSource = view.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Wrong book's title!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
