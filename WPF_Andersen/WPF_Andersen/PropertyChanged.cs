using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Andersen
{
    public class PropertyChangedEvent
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
