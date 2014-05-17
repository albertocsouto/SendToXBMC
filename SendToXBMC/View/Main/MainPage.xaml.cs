using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using SendToXBMC.Client.Requests;
using SendToXBMC.View.Settings;
using SendToXBMC.Util;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace SendToXBMC
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ShareOperation shareOperation;

        private Uri sharedWebLink;
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (SettingsManager.validateSettings())
            {
                this.InfoTextBox.Text = "Your settings seems to be OK. Try to send some Youtube videos using the Share Charm";
            }
            else
            {
                this.InfoTextBox.Text = "Your settings seems to be wrong or incomplete. Please check them.";
                return;
            }

            // It is recommended to only retrieve the ShareOperation object in the activation handler, return as
            // quickly as possible, and retrieve all data from the share target asynchronously.
            if (e.Parameter.GetType().Equals(typeof(ShareOperation)))
            {
                this.shareOperation = (ShareOperation)e.Parameter;
                if (this.shareOperation.Data.Contains(StandardDataFormats.WebLink))
                {
                    this.sharedWebLink = await this.shareOperation.Data.GetWebLinkAsync();
                    this.ProgressBar.Visibility = Visibility.Visible;
                    this.InfoTextBox.Text = String.Format("Trying to send URL {0}", this.sharedWebLink.AbsoluteUri);
                    Boolean playResult = await PlaylistRequests.playVideo(this.sharedWebLink.AbsoluteUri);
                    if (playResult)
                    {
                        this.InfoTextBox.Text = "Video URL sent to XBMC";
                    }
                    else
                    {
                        this.InfoTextBox.Text = "Cannot sent video to XBMC";
                    }
                    this.ProgressBar.Visibility = Visibility.Collapsed;
                }
            }

            PlaylistRequests.testStreamCloud();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            XBMCSettingsFlyout settings = new XBMCSettingsFlyout();
            settings.ShowIndependent();
        }
    }
}
