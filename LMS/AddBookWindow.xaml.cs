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
    /// Logika interakcji dla klasy AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
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
            tbBName.Text = "";
            tbAuthor.Text = "";
            tbGenre.Text = "";
            tbPublish.Text = "";

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(tbBName.Text == "" || tbAuthor.Text == "" || tbGenre.Text == "" || tbPublish.Text == "")
            {
                MessageBox.Show("Fill all the fields!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
                using (LibdbContext db = new LibdbContext(connectionString))
                {
                    db.Add(new Book
                    {
                        Title = tbBName.Text,
                        Author = tbAuthor.Text,
                        Genre = tbGenre.Text,
                        PublicationHouse = tbPublish.Text
                    }) ;
                    db.SaveChanges();
                    MessageBox.Show("Book added successfully!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    tbBName.Text = "";
                    tbAuthor.Text = "";
                    tbGenre.Text = "";
                    tbPublish.Text = "";


                }
            }

        }
    }
}
