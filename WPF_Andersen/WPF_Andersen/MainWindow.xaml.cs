﻿using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace WPF_Andersen
{
    public partial class MainWindow : Window
    {
        //private DatabaseContext db;
        public MainWindow()
        {
            InitializeComponent();
            //db = new DatabaseContext();
            //db.Clients.Load();
            //DatabaseGrid.ItemsSource = db.Clients.Local.ToBindingList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var allClients = new ViewModel.ClientViewModel();
            DataContext = allClients;
        }
    }
}
