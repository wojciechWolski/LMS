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

namespace LMS.BorrowWindows
{
    /// <summary>
    /// Logika interakcji dla klasy ReturnWindow.xaml
    /// </summary>
    public partial class ReturnWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";

        public ReturnWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
        /// <summary>
        /// Metoda wypełniania comboboxa nieoddanymi książkami przypisanymi do użytkownika
        /// </summary>
        private void BindCombo()
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from rt in db.Borrows where rt.StudentEnroll == Convert.ToInt32(tboxFindEnrollment.Text) && rt.BookReturn == null select rt.BookTitle;
                var listrt = query.ToList();
                for (int i = 0; i < listrt.Count; i++)
                {
                    cbRent.Items.Add(listrt[i]);
                }

            }

        }
        /// <summary>
        /// Metoda wypełniająca pola danymi studenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindEnrollment_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from u in db.Students where u.EnrollmentNumber == tboxFindEnrollment.Text select u;
                if (query.Count() > 0)
                {
                    cbRent.Items.Clear();
                    Student student = db.Students.Where(u => u.EnrollmentNumber == tboxFindEnrollment.Text).First();
                    tbFirst.Text = student.FirstName;
                    tbLast.Text = student.LastName;
                    BindCombo();
                }
                else
                {
                    MessageBox.Show("There is no student with such enrollment number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbFirst.Text = "";
                    tbLast.Text = "";
                    cbRent.Items.Clear();
                }


            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ReturnViewWindow rvw = new ReturnViewWindow();
            rvw.Show();
        }
        /// <summary>
        /// Metoda dodająca datę zwrotu do konkretnego rekordu w bazie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if(cbRent.Text != "")
            {
                using (LibdbContext db = new LibdbContext(connectionString))
                {
                    Borrow borrow = db.Borrows.Where(u => u.BookTitle == cbRent.Text && u.StudentEnroll == Convert.ToInt32(tboxFindEnrollment.Text)).First();
                    borrow.BookReturn = DateTime.Now;
                    db.SaveChanges();
                    MessageBox.Show("Book returned successfuly!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    cbRent.Items.Clear();
                    BindCombo();
                }
            }
            else
            {
                MessageBox.Show("Choose book to return!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
