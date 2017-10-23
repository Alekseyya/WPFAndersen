using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DAL.Repositories.Base;
using Model.Entities;

namespace WPF_Andersen
{
    public class UpdateViewModel : PropertyChangedEvent
    {
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
            using (IClientRepository repo = IoC.IoC.Get<IClientRepository>())
            {
                var client = SelectedClient;
                repo.Update(client);
            }
        } 

        public UpdateViewModel(Client client)
        {
            SelectedClient = client;
        }
    }
}