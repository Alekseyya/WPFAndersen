using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
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

        public ICommand UpdateMember
        {
            get
            {
                if (_updateMember == null)
                {
                    Update();
                }
                return _updateMember;
            }
        }

        public void Update()
        {
            _updateMember = new RelayCommand(obj =>
            {
                UpdateMemberOnDatabase();
                MessageBox.Show("Update competed");
            });
        }

        public void UpdateMemberOnDatabase()
        {
            var client = SelectedClient;
            _clientRepository.Update(client);
        } 

        public UpdateViewModel(Client client)
        {
            SelectedClient = client;
            _clientRepository = IoC.IoC.Get<IClientRepository>();
        }
    }
}