using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using Model.Entities;
using ViewModel.Repositories;

namespace ViewModel
{
    public class ClientViewModel: INotifyPropertyChanged
    {
        private Client selectedClient;
        private DatabaseContext db;

        private UnitOfWork _unitOfWork;
        
        public ObservableCollection<Client> Clients { get; set; }

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
            _unitOfWork = new UnitOfWork();
            db = new DatabaseContext();
            db.Clients.Load();
            Clients = db.Clients.Local;
        }


        //public void GetAllClients()
        //{
        //    db.Clients.Load();
        //    var temp = db.Clients.Local;
        //    Clients = temp;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
