using DS.WpfApp.Licensing.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DS.WpfApp.Licensing.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private string _text = "process status";
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }

        public ICommand Start => new RelayCommand(o =>
        {
            MyModel model = new MyModel(this);
            model.Task1();
        });

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
