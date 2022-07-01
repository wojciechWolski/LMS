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
    /// Logika interakcji dla klasy BorrowWindow.xaml
    /// </summary>
    public partial class BorrowWindow : Window
    {

        string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";

        public BorrowWindow()
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


        private void FindEnrollment_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from u in db.Students where u.EnrollmentNumber == tboxFindEnrollment.Text select u;
                if (query.Count() > 0)
                {
                    Student student = db.Students.Where(u => u.EnrollmentNumber == tboxFindEnrollment.Text).First();
                    tbFirst.Text = student.FirstName;
                    tbLast.Text = student.LastName;
                    tbDepartment.Text = student.Department;
                    tbEmail.Text = student.Email;
                    tbPhoneNumber.Text = student.PhoneNumber;
                }
                else
                {
                    MessageBox.Show("There is no student with such enrollment number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbFirst.Text = "";
                    tbLast.Text = "";
                    tbDepartment.Text = "";
                    tbPhoneNumber.Text = "";
                    tbEmail.Text = "";
                }


            }
        }

        private void btnLend_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirst.Text == "")
            {
                MessageBox.Show("Enter the student's data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (tbTitle.Text == "")
            {
                MessageBox.Show("Enter the book's title!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (tbDate.Text == "")
            {
                MessageBox.Show("Enter the date!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                
                using (LibdbContext db = new LibdbContext(connectionString))
                {
                    var bookcheck = from u in db.Books where u.Title == tbTitle.Text select u;
                    var stdcheck = from st in db.Students where st.EnrollmentNumber == tboxFindEnrollment.Text select st;
                    var stdlend = stdcheck.First();
                    if (bookcheck.Count() > 0)
                    {
                        var booklend = bookcheck.First();
                        DateTime? rtn = null;
                        db.Add(new Borrow
                        {
                            BookId = booklend.Id,
                            StudentId = stdlend.Id,
                            StudentEnroll = Convert.ToInt32(stdlend.EnrollmentNumber),
                            BookTitle = booklend.Title,
                            BookLend = (DateTime)tbDate.SelectedDate,
                            BookReturn = rtn

                        }) ;
                        db.SaveChanges();
                        MessageBox.Show("Book successfully lended!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("There is no such book in book's database!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void tbDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDateTextBox.Text = tbDate.SelectedDate.ToString();
        }
    }
}
