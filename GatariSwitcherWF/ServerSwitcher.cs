﻿using System.Linq;
using GatariSwitcher.Extensions;
using GatariSwitcher.Helpers;

namespace GatariSwitcher
{
    class ServerSwitcher
    {
        private readonly string serverAddress;

        public ServerSwitcher(string servAddress)
        {
            this.serverAddress = servAddress;
        }

        public void SwitchToGatari()
        {
            var lines = HostsFile.ReadAllLines();
            var result = lines.Where(x => !x.Contains("ppy.sh")).ToList();
            result.AddRange
            (
                serverAddress + " osu.ppy.sh",
                serverAddress + " c.ppy.sh",
                serverAddress + " c1.ppy.sh",
                serverAddress + " a.ppy.sh",
                serverAddress + " i.ppy.sh"
            );
            HostsFile.WriteAllLines(result);
        }

        public void SwitchToOfficial()
        {
            HostsFile.WriteAllLines(HostsFile.ReadAllLines().Where(x => !x.Contains("ppy.sh")));
        }

        /// <summary>
        /// Get current osu! server
        /// </summary>
        /// <returns>true - gatari, false - official</returns>
        public bool GetCurrentServer()
        {
            return HostsFile.ReadAllLines().Any(x => x.Contains("osu.ppy.sh") && !x.Contains("#"));
        }
    }
}
