using DS.ClassLib.LicensingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.Licensing
{
    public class LicenseDialogBuilder
    {
        private readonly string _rSAPubKey;
        private readonly string _auth;
        private readonly int _productId;
        private ActivateDialog _dialog;

        public LicenseDialogBuilder(string rSAPubKey, string auth, int productId)
        {
            _rSAPubKey = rSAPubKey;
            _auth = auth;
            _productId = productId;
        }

        public void Run()
        {
            _dialog = new ActivateDialog(_rSAPubKey, _auth, _productId );
            _dialog.Show();
        }

        public bool IsLicenseValid
        {
            get
            {
                return _dialog.IsLicenseValid;
            }
        }

    }
}
