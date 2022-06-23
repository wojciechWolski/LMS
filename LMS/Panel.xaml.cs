﻿using System;
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
    }
}