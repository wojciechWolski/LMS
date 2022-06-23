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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LMS
{

    public class HamburgerMenuControl : Control
    {

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(HamburgerMenuControl),
                new PropertyMetadata(false, OnIsOpenPropertyChanged));

        private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HamburgerMenuControl hamburgerMenu)
            {
                hamburgerMenu.OnIsOpenPropertyChanged();
            };
        }

        private void OnIsOpenPropertyChanged()
        {
            if (IsOpen)
            {
                OpenMenuAnimated();
            }
            else
            {
                CloseMenuAnimated();
            }
        }

        private void CloseMenuAnimated()
        {
            throw new NotImplementedException();
        }

        private void OpenMenuAnimated()
        {
            throw new NotImplementedException();
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }


        public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register("Content", typeof(object), typeof(HamburgerMenuControl),
            new PropertyMetadata(null));

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        static HamburgerMenuControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenuControl), new FrameworkPropertyMetadata(typeof(HamburgerMenuControl)));
        }

     
    }
}
