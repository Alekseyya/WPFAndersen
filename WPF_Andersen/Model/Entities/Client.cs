using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Model.Entities
{
    public class Client : INotifyPropertyChanged, IDataErrorInfo
    {
        
        private int _id;
        private string _firstName;
        private string _lastName;
        private int _age;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(this.FirstName))
                            error = "LastName can't be empty.";
                        var message = ValidateFirstName();
                        if (message != "Success")
                            error = message;
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(this.LastName))
                            error = "LastName can't be empty.";
                            break;
                    case "Age":
                        if ((Age < 0) || (Age > 100))
                        {
                            error = "Age can't be less then 0 or more 100";
                        }
                        break;
                }
                return error;
            }
        }

        public string ValidateFirstName()
        {
            if (string.IsNullOrEmpty(this.FirstName))
                return "LastName can't be empty.";

            Regex regex = new Regex(@"^[A-Z][a-z]{3,19}$");
            Match match = regex.Match(this.FirstName);
            if (match.Success)
            {
                return "Success";
            }
            return "First letter uppercase and legth 3 - 19";
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.FirstName)
                .HasMaxLength(40)
                .IsRequired();

            Property(x => x.LastName)
                .HasMaxLength(45)
                .IsRequired();
            //По идее добваить проперти игнор для Erro и индексатора

            Property(x => x.Age)
                .IsRequired();
        }
    }
}
