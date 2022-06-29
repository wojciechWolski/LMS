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
    /// Logika interakcji dla klasy StudentManageWindow.xaml
    /// </summary>
    public partial class StudentManageWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
        int studentIdToManage = ViewStudentsWindow.StudentId;
        public StudentManageWindow()
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
                var query = from st in db.Students where studentIdToManage == st.Id select new { st.Id, st.FirstName, st.LastName, st.Department, st.EnrollmentNumber, st.PhoneNumber, st.Email };
                dgCurrent.ItemsSource = query.ToList();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ViewStudentsWindow vsw = new ViewStudentsWindow();
            vsw.Show();
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this student?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (LibdbContext db = new LibdbContext(connectionString))
                {
                    Student student = db.Students.Where(u => u.Id == studentIdToManage).First();
                    db.Remove(student);
                    db.SaveChanges();
                    MessageBox.Show("Student deleted successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    var query = from st in db.Students where studentIdToManage == st.Id select new { st.Id, st.FirstName, st.LastName, st.Department, st.EnrollmentNumber, st.PhoneNumber, st.Email };
                    dgCurrent.ItemsSource = query.ToList();
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                if (MessageBox.Show("Are you sure you want to edit this student's info?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Student student = db.Students.Where(u => u.Id == studentIdToManage).First();
                    student.FirstName = tbFirst.Text;
                    student.LastName = tbLast.Text;
                    student.EnrollmentNumber = tbEnrollment.Text;
                    student.Department = tbDepartment.Text;
                    student.PhoneNumber = tbPhoneNumber.Text;
                    student.Email = tbEmail.Text;
                    db.SaveChanges();
                    var query = from st in db.Students where studentIdToManage == st.Id select new { st.Id, st.FirstName, st.LastName, st.Department, st.EnrollmentNumber, st.PhoneNumber, st.Email };
                    dgCurrent.ItemsSource = query.ToList();
                }
            }
        }
    }
}
