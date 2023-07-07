using DS.WpfApp.Licensing.Model;
using DS.WpfApp.Licensing.View;
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
        private readonly ActivationDialog _mainWindow;
        private readonly string _rSAPubKey;
        private readonly string _auth;
        private readonly int _productId;

        private string _licenseKey;

        public MainWindowViewModel(ActivationDialog mainWindow, string rSAPubKey, string auth, int productId)
        {
            _mainWindow = mainWindow;
            _rSAPubKey = rSAPubKey;
            _auth = auth;
            _productId = productId;
         
        }

        public string LicenseKey
        {
            get { return _licenseKey; }
            set { _licenseKey = value; OnPropertyChanged("LicenseKey"); }
        }

        public ICommand Acivate => new RelayCommand(o =>
        {
            var licence = new CryptoLicense(_rSAPubKey, _auth, _productId);
            if(licence.VerifyLicenceOffLine()) { MessageBox.Show("License already activated!"); return; }
            bool status = licence.Acitvate(LicenseKey);
            MessageBox.Show(licence.Message);
            if (status)
            {
                _mainWindow.Close();
            }

        }, o => !String.IsNullOrEmpty(_licenseKey));

        public ICommand Deactivate => new RelayCommand(o =>
        {
            var licence = new CryptoLicense(_rSAPubKey, _auth, _productId);
            licence.Deacitvate(LicenseKey);
        }, o => !String.IsNullOrEmpty(_licenseKey));

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
