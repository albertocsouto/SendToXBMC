using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YoutubeToXBMC.Util;

// La plantilla de elemento Control flotante de configuración está documentada en http://go.microsoft.com/fwlink/?LinkId=273769

namespace YoutubeToXBMC.View.Settings
{
    public sealed partial class XBMCSettingsFlyout : SettingsFlyout
    {
        public XBMCSettingsFlyout()
        {
            this.InitializeComponent();

            fillFormWithCurrentSettings();

            this.BackClick += XBMCSettingsFlyout_BackClick;
        }

        private void XBMCSettingsFlyout_BackClick(object sender, BackClickEventArgs e)
        {
            SettingsManager.saveXBMCSettings(this.HostTextBox.Text, this.PortTextBox.Text, this.UserTextBox.Text, this.PasswordTextBox.Password);
        }

        private void fillFormWithCurrentSettings()
        {
            if (SettingsManager.XBMCHost() != null)
            {
                this.HostTextBox.Text = SettingsManager.XBMCHost();
            }
            if (SettingsManager.XBMCPort() != null)
            {
                this.PortTextBox.Text = SettingsManager.XBMCPort();
            }
            if (SettingsManager.XBMCUser() != null)
            {
                this.UserTextBox.Text = SettingsManager.XBMCUser();
            }
            if (SettingsManager.XBMCPassword() != null)
            {
                this.PasswordTextBox.Password = SettingsManager.XBMCPassword();
            }
        }
    }
}
