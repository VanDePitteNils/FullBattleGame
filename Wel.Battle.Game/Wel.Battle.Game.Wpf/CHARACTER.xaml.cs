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

namespace Wel.Battle.Game.Wpf
{
    /// <summary>
    /// Interaction logic for CHARACTER.xaml
    /// </summary>
    public partial class CHARACTER : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);



        int counter = 0;
        public CHARACTER()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Loaded += ToolWindow_Loaded;
        }

        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void Proceed_Click(object sender, RoutedEventArgs e)
        {
            
            if (counter == 1)
            {
                
            }
            if (counter == 2)
            {
                MainWindow venster = new MainWindow();
                this.Visibility = Visibility.Hidden;
                venster.ShowDialog();
            }
            counter++;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
