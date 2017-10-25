using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DAL.Repositories.Base;
using Model.Entities;

namespace WPF_Andersen
{
    public class UpdateViewModel : PropertyChangedEvent
    {
        #region Рабочий код
        //private Client _selectedClient;
        //private ICommand _updateMember;

        //public Client SelectedClient
        //{
        //    get { return _selectedClient; }
        //    set
        //    {
        //        _selectedClient = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ICommand UpdateMember
        //{
        //    get
        //    {
        //        if (_updateMember == null)
        //        {
        //            Update();
        //        }
        //        return _updateMember;
        //    }
        //}

        //public void Update()
        //{
        //    _updateMember = new RelayCommand(obj =>
        //    {
        //        UpdateMemberOnDatabase();
        //        MessageBox.Show("Update competed");
        //    });
        //}

        //public void UpdateMemberOnDatabase()
        //{
        //    using (IClientRepository repo = IoC.IoC.Get<IClientRepository>())
        //    {
        //        var client = SelectedClient;
        //        repo.Update(client);
        //    }
        //}

        //public UpdateViewModel(Client client)
        //{
        //    SelectedClient = client;
        //}
        #endregion
        
        public int _level;
        public int Level { get { return _level; } set { _level = value; } }

        public List<string> SelectedDirectory;
        public List<string> TmpDirectories;
        public static List<string> ComboList;

        private ObservableCollection<string> _directories;
        public ObservableCollection<string> Directories
        {
            get
            {
                return _directories;
            }
            set
            {
                _directories = value;
                OnPropertyChanged();
            }
        }

        private ICommand _loadDirectories;
        public ICommand LoadDirectories
        {
            get
            {
                if (_loadDirectories == null)
                {
                    Load();
                }
                return _loadDirectories;
            }
        }

        private void Load()
        {
            _loadDirectories = new RelayCommand(obj =>
            {
                var path = @"C:\111";
                SetDirection(path);
                OutputAllSubfolders();
                Directories = new ObservableCollection<string>(ComboList);
            });
        }

        public void OutputAllSubfolders()
        {
            //пока не пустой список 
            while (SelectedDirectory.Count != 0)
            {
                var findElemet = SelectedDirectory[0];
                TmpDirectories.Add(findElemet);

                //Утанавлием уровень
                Level = 1;
                ComboList.Add(new ComboItem(findElemet, Level).ToString());

                CLRTree(findElemet, SelectedDirectory);


                //выйдет если проверит одно дерево
                while (TmpDirectories.Count != 0)
                {
                    var findElement = TmpDirectories.Last();
                    CLRTree(findElement, SelectedDirectory);
                }
                //ComboList.Clear(); //Очистку комбобокса пока не удалять!!
            }
        }

        public void CLRTree(string findElemet, List<string> directions)
        {
            if (TmpDirectories.Count == 0)
                TmpDirectories.Add(findElemet);

            while (true)
            {
                findElemet = FindEqualsSubfolder(findElemet, directions); //111\11\12
                if (findElemet != "")
                {
                    ++Level;
                    ComboList.Add(new ComboItem(findElemet, Level).ToString());
                    TmpDirectories.Add(findElemet);
                }
                else
                {
                    var element = TmpDirectories.Last();
                    TmpDirectories.Remove(element);
                    SelectedDirectory.Remove(element);
                    --Level;
                    break;
                }
            }
        }

        public string FindEqualsSubfolder(string findElement, List<string> directories)
        {
            foreach (var path in directories)
            {
                if (path.Contains(findElement + "\\"))
                {
                    return path;
                }
            }
            return "";
        }

        public void SetDirection(string path)
        {
            SelectedDirectory = Directory.GetDirectories(path, "*", SearchOption.AllDirectories).ToList();
            TmpDirectories = new List<string>();
            ComboList = new List<string>();
        }

        public async Task<List<string>> GetDirectories(string path, string searchPattern = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            //Если выбрана С:\\111\11
            if (searchOption == SearchOption.TopDirectoryOnly)
                return await Task.Run(() =>
                {
                    return Directory.GetDirectories(path, searchPattern).ToList();
                });

            //Все директории
            return await Task.Run(() =>
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            });
        }

        public async Task<MyDirectory> GetDirectoryInfo(string path)
        {
            return await Task.Run(() =>
            {
                var dir = new DirectoryInfo(path);
                var myDirectory = new MyDirectory();
                if (dir.Parent != null)
                {
                    myDirectory.Parent = dir.Parent.ToString();
                    myDirectory.Child = dir.Name;
                }
                return myDirectory;
            });
        }

        public UpdateViewModel()
        {
            
        }
        
    }
}