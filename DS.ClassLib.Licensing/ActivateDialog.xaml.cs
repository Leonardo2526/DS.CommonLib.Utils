using DS.ClassLib.Licensing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DS.ClassLib.Licensing
{
    /// <summary>
    /// Interaction logic for ActivateDialog.xaml
    /// </summary>
    public partial class ActivateDialog : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public ActivateDialog(string rSAPubKey, string auth, int productId)
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(this, rSAPubKey, auth, productId);
            DataContext = _viewModel;
        }

        public bool IsLicenseValid
        {
            get
            {
                return _viewModel.IsLicenseValid;
            }
        }

        public string RSAPubKey { get; set; }
        public string Auth { get; set; }
        public int ProductId { get; set; }
    }
}
