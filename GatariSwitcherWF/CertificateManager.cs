using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GatariSwitcher
{
    class CertificateManager
    {
        public bool GetStatus()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            var c = store.Certificates.Find(X509FindType.FindBySubjectName, "*.ppy.sh", true);
            bool result = (c.Count > 0);
            store.Close();

            return result;
        }

        public void Install()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);

            var certificate = new X509Certificate2(GatariSwitcherWF.Resource1.gatari);
            store.Add(certificate);

            store.Close();
        }

        public void Uninstall()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);

            var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, "*.ppy.sh", true);
            try
            {
                foreach (var c in certificates)
                {
                    store.Remove(c);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                store.Close();
            }
        }
    }
}
