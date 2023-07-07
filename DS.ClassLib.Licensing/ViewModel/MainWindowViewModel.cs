using DS.ClassLib.Licensing;
using DS.ClassLib.LicensingModel;
using DS.ClassLib.VarUtils;
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

namespace DS.ClassLib.Licensing.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ActivateDialog _mainWindow;
        private readonly string _rSAPubKey;
        private readonly string _auth;
        private readonly int _productId;
        private readonly Action _action;
        private string _licenseKey;

        public MainWindowViewModel(ActivateDialog mainWindow, string rSAPubKey, string auth, int productId, Action action)
        {
            _mainWindow = mainWindow;
            _rSAPubKey = rSAPubKey;
            _auth = auth;
            _productId = productId;
            _action = action;
        }

        public string LicenseKey
        {
            get { return _licenseKey; }
            set { _licenseKey = value; OnPropertyChanged("LicenseKey"); }
        }
        public bool IsLicenseValid { get; private set; }


        public ICommand Acivate => new RelayCommand(o =>
        {
            var licence = new CryptoLicense(_rSAPubKey, _auth, _productId);
            IsLicenseValid = licence.Acitvate(LicenseKey);
            MessageBox.Show(licence.Message);
            if (IsLicenseValid)
            {
                _mainWindow.Close();
                _action?.Invoke();
            }

        }, o => !String.IsNullOrEmpty(_licenseKey));


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
