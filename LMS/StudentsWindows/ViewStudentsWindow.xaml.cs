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
    /// Logika interakcji dla klasy ViewStudentsWindow.xaml
    /// </summary>
    public partial class ViewStudentsWindow : Window
    {
        readonly string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
        public static int StudentId { get; internal set; }
        public ViewStudentsWindow()
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from st in db.Students select new { st.Id, st.FirstName, st.LastName, st.Department, st.EnrollmentNumber, st.PhoneNumber, st.Email };
                dgStudentView.ItemsSource = query.ToList();
            }

        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from st in db.Students where tbStudentToFind.Text == st.LastName select new { st.Id, st.FirstName, st.LastName, st.Department, st.EnrollmentNumber, st.PhoneNumber, st.Email };
                dgStudentView.ItemsSource = query.ToList();
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from st in db.Students select new { st.Id, st.FirstName, st.LastName, st.Department, st.EnrollmentNumber, st.PhoneNumber, st.Email };
                dgStudentView.ItemsSource = query.ToList();
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

        private void btnManageGo_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbIdManage.Text, out int stdId))
            {
                StudentId = stdId;
                string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
                using (LibdbContext db = new LibdbContext(connectionString))
                {
                    var query = from u in db.Students where StudentId == u.Id select u;
                    if (query.Count() == 0)
                    {
                        MessageBox.Show("There is no such id in the student database!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        this.Hide();
                        StudentManageWindow smw = new StudentManageWindow();
                        smw.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Number is expected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
