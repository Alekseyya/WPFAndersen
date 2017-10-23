using System;
using Model.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private IClientRepository _clientRepository;
        private RelayCommand _addMember;
        private RelayCommand _deleteMember;

        private CancellationTokenSource tokenSource;
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
            // получается мы тут сохдаем объект
            _clientRepository = IoC.IoC.Get<IClientRepository>();
        }

        private void ResetSourceAndToken()
        {
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }
        public void Add()
        {
            var client = new Client()
            {
                FirstName = ClientTest.FirstName,
                LastName = ClientTest.LastName,
                Age = ClientTest.Age
            };
            _addMember = new RelayCommand(obj =>
            {
                AddMemberOnDatabase(client);
            });
        }

        public void AddMemberOnDatabase(Client client)
        {
            if (!_clientRepository.HasClientOnDatabase(client))
            {
                _clientRepository.Create(client); // например после этого надо делать диспоуз?
                SelectedClient = client;
            }
            else
                MessageBox.Show("Такой пользователь уже существует");
        }

        public void Delete()
        {
            _deleteMember = new RelayCommand(obj =>
            {
                Client client = obj as Client;
                if (client != null)
                {
                    DeleteMemberOnDatabase(client);
                }
            });
        }

        public void DeleteMemberOnDatabase(Client client)
        {
            _clientRepository.Delete(client.Id);
            Clients.Remove(client);
        }

       
        //Недоделано..
        public async Task Load()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                if (token.IsCancellationRequested)
                {
                    MessageBox.Show("Операция отменена");
                    return;
                }
                Clients = new ObservableCollection<Client>(_clientRepository.GetList());
            }, token);
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show(elapsedTime);
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
