using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using SendToXBMC.View.Settings;
using SendToXBMC.Util;

// La plantilla Aplicación vacía está documentada en http://go.microsoft.com/fwlink/?LinkId=234227

namespace SendToXBMC
{
    /// <summary>
    /// Proporciona un comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
    
                }

                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            SettingsPane.GetForCurrentView().CommandsRequested += (_, exception) =>
            {
                exception.Request.ApplicationCommands.Add(
                new SettingsCommand("P", ResourcesManager.LocalizedString("PrivacyPolicy"), OpenPrivacyPolicy));
                exception.Request.ApplicationCommands.Add(
                new SettingsCommand("X", ResourcesManager.LocalizedString("XBMCSettings"), OpenXBMCSettings));
            };

            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

     
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            var rootFrame = new Frame();
            rootFrame.Navigate(typeof(MainPage), args.ShareOperation);
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private void OpenPrivacyPolicy(IUICommand command)
        {
            PrivacyPolicySettingsFlyout settings = new PrivacyPolicySettingsFlyout();
            settings.Show();
        }

        private void OpenXBMCSettings(IUICommand command)
        {
            XBMCSettingsFlyout settings = new XBMCSettingsFlyout();
            settings.Show();
        }
    }
}
