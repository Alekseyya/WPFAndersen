using System.Collections.ObjectModel;
using System.Windows;
using DAL.Repositories.Base;
using Model.Entities;

namespace WPF_Andersen
{
    public class UpdateViewModel : PropertyChangedEvent
    {
        private IClientRepository _clientRepository;
        private Client _selectedClient;
        private RelayCommand _updateMember;

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand UpdateMember
        {
            get
            {
                return _updateMember ??
                       (_updateMember = new RelayCommand(obj =>
                       {
                           var client = SelectedClient;
                           _clientRepository.Update(client);
                           SelectedClient = client;
                           MessageBox.Show("Update competed");
                       }));
            }
        }

        public UpdateViewModel(Client client)
        {
            SelectedClient = client;
            _clientRepository = IoC.IoC.Get<IClientRepository>();
        }
    }
}