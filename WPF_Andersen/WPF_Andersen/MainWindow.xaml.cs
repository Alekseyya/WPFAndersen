using System.Windows;
using System.Windows.Input;

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
