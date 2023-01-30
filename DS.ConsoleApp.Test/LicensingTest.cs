using DS.ClassLib.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal class LicensingTest
    {
        public static void Run()
        {
            string _rSAPubKey = "<RSAKeyValue><Modulus>x8ylENxp18Nk5IOJL9D439AZifcdmVMjKcHPZJwOOhliSjX72KJBsl+EkwuKi6Dxe4jC3bNGi2qjA6nNvjhfTrYVsS8ULwxQMUPXkxmDhIizP6H7P3l7FYtxCZU5fslu1kYfqAYzWXh42lOYrrSRnspBylFIp7Dwut7OWaNlYkec3faqEVZZTyE9Qsefs/M7NqPihzTGVZkZ8ACWg4jbIPf1yRd6LRwEP23N52HCQSMV3VTU9ftZBtQ77tIl/KBNoiRD5wrTrNF3uNlbJGO8cyBR0nMdBgsOxky8LZBXqMj7K1u0WbZO5O1GMz+RXAJcAFv+UyqpJVr3Fcfb9xArpQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            string _auth = "WyIzNjUxODExOCIsIlVSU1lrTks0anBjTVpJWDdGekJ1ZXNoSHBxS013bkw5UENkMi9qTGgiXQ==";
            int _productId = 18607;

            var builder =  new LicenseDialogBuilder(_rSAPubKey, _auth, _productId);
            builder.Run();
            Console.WriteLine(builder.IsLicenseValid);
        }
    }
}
