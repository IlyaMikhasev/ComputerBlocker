using System;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Messaging;
using System.Windows.Input;
using System.Threading;

namespace ComputerBlocker
{
    /// <summary>
    /// Логика взаимодействия для LockedWindow.xaml
    /// </summary>
    public partial class LockedWindow : Window
    {
        private bool flagClose = true;
        
        public LockedWindow()
        {
            InitializeComponent();
        }
        public void Unlock() {
                flagClose = false;
                this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SettingsModel.Password == pass.Password)
            {
                Unlock();
            }
            else
                TextPass.Text = "неверный пароль";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = flagClose;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = false;
            this.Topmost = true;
        }
    }
}
