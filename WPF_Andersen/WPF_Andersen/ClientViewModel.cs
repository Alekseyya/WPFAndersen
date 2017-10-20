using DAL.Repositories;
using Model.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL.Repositories.Base;
using WPF_Andersen.IoC;

namespace WPF_Andersen
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        private Client selectedClient;
        private IClientRepository _clientRepository;

        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged();
            }
        }

        public ClientViewModel()
        {
            _clientRepository = IoC.IoC.Get<IClientRepository>();
        }

        public void Load()
        {
            Clients = new ObservableCollection<Client>(_clientRepository.GetList());
        }
        public void Open(Client client)
        {
            //var updateWindow = new UpdateWindow();
            // var viewModel = new UpdateViewModel(client)
            // updateWindow.DataContext = viewModel;

            //updateWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
