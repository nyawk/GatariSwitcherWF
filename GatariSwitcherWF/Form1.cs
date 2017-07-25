using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//
using GatariSwitcher.Extensions;
using GatariSwitcher.Helpers;
using GatariSwitcher;

namespace GatariSwitcherWF
{
    public partial class Form1 : Form
    {
        bool certStatus = false;
        bool servStatus = false;

        string gatariAddress = "173.212.240.174";

        public Form1()
        {
            InitializeComponent();
            try
            {
                string newAddress = GeneralHelper.GetGatariAddress();
                gatariAddress = newAddress;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось получить адрес сервера:\r\n" + ex.Message);
            }
            CheckStatus();
        }

        private void CheckStatus()
        {
            CheckServerStatus();
            CheckCertStatus();
        }

        private void CheckServerStatus()
        {
            var switcher = new ServerSwitcher(gatariAddress);
            try
            {
                servStatus = switcher.GetCurrentServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения текущего сервера:\r\n" + ex.Message);
            }
            button1.Text = servStatus ? "Перейти на официальный сервер" : "Перейти на гатари";
        }       

        private void CheckCertStatus()
        {
            var manager = new CertificateManager();
            try
            {
                certStatus = manager.GetStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения статуса сертификата:" + ex.Message);
            }

            button2.Text = certStatus ? "Удалить сертификат" : "Установить сертификат";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var switcher = new ServerSwitcher(gatariAddress);
            try
            {
                if (servStatus)
                {
                    switcher.SwitchToOfficial();
                }
                else
                {
                    switcher.SwitchToGatari();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            CheckStatus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var manager = new CertificateManager();
            try
            {
                if (certStatus)
                {
                    manager.Uninstall();
                }
                else
                {
                    manager.Install();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            CheckStatus();
        }
    }
}
