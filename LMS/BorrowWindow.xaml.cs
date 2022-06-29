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
            if(tbFirst.Text == "")
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();

            }
        }
    }
}
