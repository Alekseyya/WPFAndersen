using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DAL.Repositories;
using DAL.Repositories.Base;
using Model.Entities;
using Ninject;


namespace ViewModel
{
    public class ClientViewModel: INotifyPropertyChanged
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
            _clientRepository = IoC.Get<IClientRepository>();
        }

        public void Load()
        {
            Clients = new ObservableCollection<Client>(_clientRepository.GetList());
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Open(Client client)
        {
            //var updateWindow = new UpdateWindow();
            // var viewModel = new UpdateViewModel(client)
            // updateWindow.DataContext = viewModel;
            
            //updateWindow.Show();
        }
    }

    static class IoC
    {
        private static IKernel _kernel;

        static IoC()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IClientRepository>().To<ClientRepository>();
            //_kernel.bind.to.....

        }
        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
