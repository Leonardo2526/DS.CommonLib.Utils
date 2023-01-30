using DS.WpfApp.Licensing.Model;
using DS.WpfApp.Licensing.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace DS.WpfApp.Licensing.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ActivationDialog : Window
    {
        public string _rSAPubKey = "<RSAKeyValue><Modulus>x8ylENxp18Nk5IOJL9D439AZifcdmVMjKcHPZJwOOhliSjX72KJBsl+EkwuKi6Dxe4jC3bNGi2qjA6nNvjhfTrYVsS8ULwxQMUPXkxmDhIizP6H7P3l7FYtxCZU5fslu1kYfqAYzWXh42lOYrrSRnspBylFIp7Dwut7OWaNlYkec3faqEVZZTyE9Qsefs/M7NqPihzTGVZkZ8ACWg4jbIPf1yRd6LRwEP23N52HCQSMV3VTU9ftZBtQ77tIl/KBNoiRD5wrTrNF3uNlbJGO8cyBR0nMdBgsOxky8LZBXqMj7K1u0WbZO5O1GMz+RXAJcAFv+UyqpJVr3Fcfb9xArpQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public string _auth = "WyIzNjUxODExOCIsIlVSU1lrTks0anBjTVpJWDdGekJ1ZXNoSHBxS013bkw5UENkMi9qTGgiXQ==";
        public int _productId = 18607;

        public ActivationDialog()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this, _rSAPubKey, _auth, _productId);
        }

    }
}
