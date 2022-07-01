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
using System.Data;
using System.Data.SqlClient;
using LMS.Models;
using System.Security.Cryptography;

namespace LMS
{
    /// <summary>
    /// Logika interakcji dla klasy Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Hashowanie hasła
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        /// <summary>
        /// Zahashowane hasło do stringa
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        /// <summary>
        /// Metoda aktywowana po przytrzymaniu przycisku na oknie, umożliwiająca poruszanie okienkiem po ekranie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        /// <summary>
        /// Metoda aktywowana po kliknięciu - zamykająca bieżące okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda aktywowana przyciskiem - umożliwiająca rejestację. Po uzyskaniu walidacji loginu oraz hasła, łączy się z bazą, aby dodać rekord z danymi logowania dla nowego użytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {

            if (tboxUsername.Text.Length < 3)
            {
                MessageBox.Show("Login is too short! At least 3 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (policy.IsValid(boxPassword.Password))
                {
                    string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
                    using (LibdbContext db = new LibdbContext(connectionString))
                    {
                        var query = from u in db.Admins where u.Username == tboxUsername.Text select u;
                        if (query.Count() > 0)
                        {
                            MessageBox.Show("Login already in use!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if (boxPassword.Password != boxPassword2.Password)
                            {
                                MessageBox.Show("Passwords are different!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                var pass = GetHashString(boxPassword.Password);

                                db.Add(new Admin
                                {
                                    Username = tboxUsername.Text,
                                    Password = pass
                                }) ;
                                db.SaveChanges();
                                MessageBox.Show("Sign up successful!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Password is too weak! Check our password policy.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }

        /// <summary>
        /// Metoda aktywowana przyciskiem - zamyka bieżące okno i kieruje nas do MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
