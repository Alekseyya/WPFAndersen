using DAL.Repositories;
using Model.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DAL.Repositories.Base;
using WPF_Andersen.IoC;

namespace WPF_Andersen
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        private Client _selectedClient;
        private IClientRepository _clientRepository;
        private RelayCommand _addMember;
        private RelayCommand _deleteMember;

        private string _firstName;
        private string _lastName;
        private int _age = 0;
        
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public int Age {
            get { return _age; }
            set { _age = value; }
        }

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
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddMember
        {
            get
            {
                return _addMember ??
                       (_addMember = new RelayCommand(obj =>
                       {
                           AddMemberOnDatabase();
                       }));
            }
        }

        public void AddMemberOnDatabase()
        {
            var client = new Client()
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = Age
            };
            if (!HaveClientOnDatabase(client))
            {
                _clientRepository.Create(client);
                SelectedClient = client;
            }
            else
                MessageBox.Show("Такой пользователь уже существует");

        }
        public bool HaveClientOnDatabase(Client client)
        {
            var flag = _clientRepository
                .GetList().Where(cl => cl.FirstName == client.FirstName && cl.LastName == client.LastName && cl.Age == client.Age)
                          .Any();
            return flag;
        }
        public RelayCommand DeleteMember
        {
            get
            {
                return _deleteMember ??
                       (_deleteMember = new RelayCommand(obj =>
                       {
                           var client = SelectedClient;
                           _clientRepository.Delete(SelectedClient.Id);
                           SelectedClient = client;
                       }));
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
        public void Open(object client)
        {
            var updateWindow = new UpdateWindow();
            var viewModel = new UpdateViewModel((Client)client);
            updateWindow.DataContext = viewModel;
            updateWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
