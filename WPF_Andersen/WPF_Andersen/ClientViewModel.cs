using System;
using DAL.Repositories;
using Model.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DAL.Repositories.Base;
using WPF_Andersen.IoC;

namespace WPF_Andersen
{
    public class ClientViewModel : PropertyChangedEvent
    {
        private Client _selectedClient;
        private IClientRepository _clientRepository;
        private RelayCommand _addMember;
        private RelayCommand _deleteMember;

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

        public RelayCommand DeleteMember
        {
            get
            {
                return _deleteMember ??
                       (_deleteMember = new RelayCommand(obj =>
                           {
                               Client client = obj as Client;
                               if (client != null)
                               {
                                   _clientRepository.Delete(client.Id);
                                   Clients.Remove(client);

                               }
                           }));
            }
        }

        public ICommand IP { get; set; }


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
                FirstName = ClientTest.FirstName,
                LastName = ClientTest.LastName,
                Age = ClientTest.Age
            };
            if (!HaveClientOnDatabase(client))
            {
                _clientRepository.Create(client); // например после этого надо делать диспоуз?
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
       
        public ClientViewModel()
        {
            _clientTest = new Client();
            // получается мы тут сохдаем объект
            _clientRepository = IoC.IoC.Get<IClientRepository>();
        }

        public async Task Load()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await Task.Run(() =>
            {
                //Thread.Sleep(5000);
                Clients = new ObservableCollection<Client>(_clientRepository.GetList());
            });
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

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged([CallerMemberName]string prop = "")
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //}
    }
}
