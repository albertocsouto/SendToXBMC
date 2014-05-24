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
using SendToXBMC.Util;
using SendToXBMC.Client.Requests;


namespace SendToXBMC.View.Settings
{
    public sealed partial class XBMCSettingsFlyout : SettingsFlyout
    {
        public XBMCSettingsFlyout()
        {
            this.InitializeComponent();

            fillFormWithCurrentSettings();
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

        private async void SaveAndTestButton_Click(object sender, RoutedEventArgs e)
        {
            this.TestResultTextBlock.Text = "";
            SettingsManager.saveXBMCSettings(this.HostTextBox.Text, this.PortTextBox.Text, this.UserTextBox.Text, this.PasswordTextBox.Password);
            this.TestProgressRing.Visibility = Visibility.Visible;
            bool pingResult = await JSONRPCRequests.Ping();
            this.TestProgressRing.Visibility = Visibility.Collapsed;
            if (pingResult)
            {
                this.TestResultTextBlock.Text = ResourcesManager.LocalizedString("XBMCSettingsTestOk");
            }
            else
            {
                this.TestResultTextBlock.Text = ResourcesManager.LocalizedString("XBMCSettingsTestKo");
            }
        }
    }
}
