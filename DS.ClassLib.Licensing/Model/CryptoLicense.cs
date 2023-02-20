using SKM.V3;
using SKM.V3.Accounts;
using SKM.V3.Methods;
using SKM.V3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DS.ClassLib.LicensingModel
{
    public class CryptoLicense
    {
        private readonly string _rSAPubKey;
        private readonly string _auth;
        private readonly int _productId;

        public CryptoLicense(string rSAPubKey, string auth, int productId)
        {
            _rSAPubKey = rSAPubKey;
            _auth = auth;
            _productId = productId;
        }

        public string Message { get; private set; }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string LicensePath
        {
            get 
            {
                return AssemblyDirectory + "\\licensekey.skm";
            }
        }

        public bool Acitvate(string licenseKey)
        {
            var result = Key.Activate(token: _auth, parameters: new ActivateModel()
            {
                Key = licenseKey,
                ProductId = _productId,
                Sign = true,
                MachineCode = Helpers.GetMachineCodePI(v: 2)
            });

            if (result == null || result.Result == ResultType.Error ||
                !result.LicenseKey.HasValidSignature(_rSAPubKey).IsValid())
            {
                Message = "Sorry, the license does not work.";
            }
            else
            {
                Message = "The license is valid! Now you can use application.";
                result.LicenseKey.SaveToFile(LicensePath);
            }

            Debug.WriteLine(Message);
            return result.Result == ResultType.Success;
        }


        public bool Deacitvate(string licenseKey)
        {
            var result = Key.Deactivate(token: _auth, parameters: new DeactivateModel()
            {
                Key = licenseKey,
                ProductId = _productId,
                MachineCode = Helpers.GetMachineCodePI(v: 2)
            });

            if (result == null || result.Result == ResultType.Error)
            {
                Message = "Failed to deactivate license!";
            }
            else
            {
                Message = "The license was deactivated";
            }

            Debug.WriteLine(Message);
            return result.Result == ResultType.Success;
        }

        public bool VerifyLicenceOffLine()
        {
            var licensefile = new LicenseKey();
            var machineCode = Helpers.GetMachineCodePI(v: 2);

            bool isValid;
            if (isValid = licensefile.LoadFromFile(LicensePath)
                          .HasValidSignature(_rSAPubKey)
                          .IsValid() &&
                          Helpers.
                          IsOnRightMachine(licensefile.
                          LoadFromFile(LicensePath), machineCode))
            {
                Message = "The license is valid!";
            }
            else
            {
                Message = "The license does not work.";
            }

            Debug.WriteLine(Message);
            return isValid;
        }

    }
}
