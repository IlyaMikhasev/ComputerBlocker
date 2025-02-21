using System;
using System.Windows;

namespace ComputerBlocker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           InitializeComponent();
            ShowInTaskbar = false;
        }
    }
}
