using LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Logika interakcji dla klasy AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public AddStudentWindow()
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

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbFirst.Text = "";
            tbLast.Text = "";
            tbEnrollment.Text = "";
            tbDepartment.Text = "";
            tbPhoneNumber.Text = "";
            tbEmail.Text = "";
        }
        /// <summary>
        /// Metoda, która po walidacji dodaje rekord studenta do bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirst.Text == "" || tbLast.Text == "" || tbEnrollment.Text == "" || tbDepartment.Text == "" || tbEmail.Text == "" || tbPhoneNumber.Text == "")
            {
                MessageBox.Show("Fill all the fields!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var email = new EmailAddressAttribute();
                if (tbPhoneNumber.Text.Length != 9)
                {
                    MessageBox.Show("Number must have nine digits!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (email.IsValid(tbEmail.Text) == false)
                {
                    MessageBox.Show("Email address is invalid!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (tbEnrollment.Text.Length > 5)
                {
                    MessageBox.Show("Enrollment number can't be longer than five digits!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
                    using (LibdbContext db = new LibdbContext(connectionString))
                    {
                        db.Add(new Student
                        {
                            FirstName = tbFirst.Text,
                            LastName = tbLast.Text,
                            Department = tbDepartment.Text,
                            EnrollmentNumber = tbEnrollment.Text,
                            PhoneNumber = tbPhoneNumber.Text,
                            Email = tbEmail.Text,
                        });
                        db.SaveChanges();
                        MessageBox.Show("Student added successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                        tbFirst.Text = "";
                        tbLast.Text = "";
                        tbEnrollment.Text = "";
                        tbDepartment.Text = "";
                        tbPhoneNumber.Text = "";
                        tbEmail.Text = "";
                    }
                }




            }

        }
    }
}







