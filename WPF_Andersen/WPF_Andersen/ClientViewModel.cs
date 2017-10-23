using System;
using System.Collections.Generic;
using Model.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DAL.Repositories.Base;

namespace WPF_Andersen
{
    public class ClientViewModel : PropertyChangedEvent
    {
        private Client _selectedClient;
        private RelayCommand _addMember;
        private RelayCommand _deleteMember;

        public CancellationTokenSource tokenSource;
        private CancellationToken token;

        private string _firstName;
        private string _lastName;
        private int _age = 0;

        private Client _clientTest;

        public Client ClientTest{
            get { return _clientTest; }
            set
            {
                _clientTest = value;
                OnPropertyChanged();
            } }
        //public string FirstName
        //{
        //    get { return _firstName; }
        //    set
        //    {
        //        _firstName = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public string LastName
        //{
        //    get { return _lastName; }
        //    set { _lastName = value; }
        //}

        //public int Age {
        //    get { return _age; }
        //    set { _age = value; }
        //}

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

        public ICommand DeleteMember
        {
            get
            {
                if (_deleteMember == null)
                {
                    Delete();
                }
                return _deleteMember;
            }
        }
        public ICommand AddMember {
            get {
                if (_addMember == null)
                {
                    Add();
                }
                return _addMember;
            }
        }
       
        public ClientViewModel()
        {
            ResetSourceAndToken();
            _clientTest = new Client();
        }

        public void ResetSourceAndToken()
        {
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }
        public void Add()
        {
            _addMember = new RelayCommand(async obj =>
            {
                var client = new Client()
                {
                    FirstName = ClientTest.FirstName,
                    LastName = ClientTest.LastName,
                    Age = ClientTest.Age
                };
                await AddMemberOnDatabase(client);
            });
        }

        public async Task AddMemberOnDatabase(Client client)
        {
            await Task.Run(() =>
            {
                using (IClientRepository repo = IoC.IoC.Get<IClientRepository>())
                {
                    if (!repo.HasClientOnDatabase(client))
                        repo.Create(client);
                    else
                        MessageBox.Show("Такой пользователь уже существует");
                }
            });
        }

        public void Delete()
        {
            _deleteMember = new RelayCommand(async obj =>
            {
                Client client = obj as Client;
                if (client != null)
                {
                    await DeleteMemberOnDatabase(client);
                }
            });
        }

        public async Task DeleteMemberOnDatabase(Client client)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                using (IClientRepository repo = IoC.IoC.Get<IClientRepository>())
                {
                    repo.Delete(client.Id);
                }
            });
            Clients.Remove(client);
        }
       
        public async Task Load()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await LoadAsync();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show(elapsedTime);
        }

        private async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                if (token.IsCancellationRequested)
                {
                    MessageBox.Show("Операция отменена");
                    return;
                }
                var listClients = new List<Client>();
                using (IClientRepository repo = IoC.IoC.Get<IClientRepository>())
                {
                    listClients = repo.GetList().ToList();
                }
                Clients = new ObservableCollection<Client>(listClients);
            }, token);
        }

        public void Open(object client)
        {
            var updateWindow = new UpdateWindow();
            var viewModel = new UpdateViewModel((Client)client);
            updateWindow.DataContext = viewModel;
            updateWindow.Show();
        }

        //Добавить в метод выхода(красный крестик) token.Cancel()
    }
}
