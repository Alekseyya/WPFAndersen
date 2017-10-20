using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPF_Andersen
{
    public partial class MainWindow : Window
    {
        //private ViewModel.ClientViewModel _allClients;
        //private DatabaseContext db;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ClientViewModel();

            DoubleAnimation buttonAnimation = new DoubleAnimation();
            buttonAnimation.From = UpdateButton.ActualWidth;
            buttonAnimation.To = 150;
            buttonAnimation.Duration = TimeSpan.FromSeconds(3);
            UpdateButton.BeginAnimation(Button.WidthProperty, buttonAnimation);
            // _allClients = new ViewModel.ClientViewModel();

            //db = new DatabaseContext();
            //db.Clients.Load();
            //DatabaseGrid.ItemsSource = db.Clients.Local.ToBindingList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ClientViewModel;
            if(viewModel == null) return;
            viewModel.Load();
            DeleteMemmberButton.Visibility = Visibility.Visible;
            AddMember.Visibility = Visibility.Visible;
        }


        private void DoubleMouseClickOnRowDataGrid(object sender, MouseButtonEventArgs e)
        {
            if (DatabaseGrid.SelectedItem == null) return;
            var selectedClient = DatabaseGrid.SelectedItem;
            var viewModel = DataContext as ClientViewModel;
            viewModel.Open(selectedClient);


            //UpdateWindow updateWindow = new UpdateWindow();
            //updateWindow.Show();
            //DataContext = selectedClient;
            //DataContext = _allClients;
        }
    }
}
