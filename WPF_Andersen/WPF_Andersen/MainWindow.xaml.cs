using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ViewModel;

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
            // DataContext = _allClients;
        }


        private void DoubleMouseClickOnRowDataGrid(object sender, MouseButtonEventArgs e)
        {
            if (DatabaseGrid.SelectedItem == null) return;
            var selectedClient = DatabaseGrid.SelectedItem;
            UpdateWindow updateWindow = new UpdateWindow();
            updateWindow.Show();
            DataContext = selectedClient;
            //DataContext = _allClients;
        }
    }
}
