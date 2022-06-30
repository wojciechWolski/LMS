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
    /// Logika interakcji dla klasy Panel.xaml
    /// </summary>
    public partial class Panel : Window
    {
        public Panel()
        {
            InitializeComponent();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (burger.IsOpen)
                burger.IsOpen = false;
            else
                burger.IsOpen = true;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void exitHMI_Checked(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to exit?","Confirmation",MessageBoxButton.YesNo,MessageBoxImage.Warning)==MessageBoxResult.Yes)
                this.Close();
          
            
        }

        private void introHMI_Checked(object sender, RoutedEventArgs e)
        {
           if(this.tbGreet.Visibility == Visibility.Collapsed)
            {
                this.tbGreet.Visibility = Visibility.Visible;
                this.tbGreet2.Visibility = Visibility.Visible;
            }

        }

        private void introHMI_Unchecked(object sender, RoutedEventArgs e)
        {
             
            if(this.tbGreet.Visibility == Visibility.Visible)
            {
                this.tbGreet.Visibility = Visibility.Collapsed;
                this.tbGreet2.Visibility = Visibility.Collapsed;
            }
                
            
        }

        private void bcHMI_Checked(object sender, RoutedEventArgs e)
        {
            if(this.btnAddBook.Visibility == Visibility.Collapsed)
            {
                this.bookImg.Visibility = Visibility.Visible;
                this.btnViewBooks.Visibility=Visibility.Visible;
                this.btnAddBook.Visibility=Visibility.Visible;
            }
        }

        private void bcHMI_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.btnAddBook.Visibility == Visibility.Visible)
            {
                this.bookImg.Visibility = Visibility.Collapsed;
                this.btnViewBooks.Visibility=Visibility.Collapsed;
                this.btnAddBook.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow abw = new AddBookWindow();
            abw.Show();
        }

        private void btnViewBooks_Click(object sender, RoutedEventArgs e)
        {
            ViewBooksWindow vbw = new ViewBooksWindow();
            vbw.Show();
        }

        private void studentsHMI_Checked(object sender, RoutedEventArgs e)
        {
            if (this.btnAddStudent.Visibility == Visibility.Collapsed)
            {
                this.studentImg.Visibility = Visibility.Visible;
                this.btnViewStudents.Visibility = Visibility.Visible;
                this.btnAddStudent.Visibility = Visibility.Visible;
            }
        }

        private void studentsHMI_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.btnAddStudent.Visibility == Visibility.Visible)
            {
                this.studentImg.Visibility = Visibility.Collapsed;
                this.btnViewStudents.Visibility = Visibility.Collapsed;
                this.btnAddStudent.Visibility = Visibility.Collapsed;
            }

        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow asw = new AddStudentWindow();
            asw.Show();
        }

        private void btnViewStudents_Click(object sender, RoutedEventArgs e)
        {
            ViewStudentsWindow vsw = new ViewStudentsWindow();
            vsw.Show();
        }

        private void borrowHMI_Checked(object sender, RoutedEventArgs e)
        {
            if (this.borrowImg.Visibility == Visibility.Collapsed)
                this.borrowImg.Visibility = Visibility.Visible;
            BorrowWindow bw = new BorrowWindow();
            bw.Show();
        }

        private void borrowHMI_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.borrowImg.Visibility == Visibility.Visible)
                    this.borrowImg.Visibility = Visibility.Collapsed;

        }

        private void returnHMI_Checked(object sender, RoutedEventArgs e)
        {
            if (this.returnImg.Visibility == Visibility.Collapsed)
                this.returnImg.Visibility = Visibility.Visible;
        }

        private void returnHMI_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.returnImg.Visibility == Visibility.Visible)
                this.returnImg.Visibility = Visibility.Collapsed;
        }
    }
}
