using System.Net;

namespace GatariSwitcher
{
    static class GeneralHelper
    {
        const string serverAddressUrl = @"https://osu.gatari.pw/api/v1/ip";

        public static string GetGatariAddress()
        {
            using (var client = new WebClient())
            {
                string result = client.DownloadString(serverAddressUrl).Trim();

                return result;
            }
        }

    }
}
