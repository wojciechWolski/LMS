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
        string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this item?","Confirmation",MessageBoxButton.YesNoCancel,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
              /*  using (LibdbContext db = new LibdbContext(connectionString)
                {
                    throw new NotImplementedException();
                }*/
            }



        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ViewBooksWindow vbw = new ViewBooksWindow();
            vbw.Show();
        }
    }
    }
