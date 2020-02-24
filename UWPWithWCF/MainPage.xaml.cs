using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using UWPWithWCF.BrightnessService;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPWithWCF
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private BrightnessNotificationServiceClient brightnessClient { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void BrightnessClient_NotifyBrightnessReceived(object sender, NotifyBrightnessReceivedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                brightnessValue.Text = e.value.ToString();
            });
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }

            var binding = new NetTcpBinding(SecurityMode.None);
            var address = new EndpointAddress("net.tcp://localhost:8001/BrightnessNotificationService");
            brightnessClient = new BrightnessNotificationServiceClient(binding, address);

            await brightnessClient.SubscribeBrightnessNotificationAsync();
            brightnessClient.NotifyBrightnessReceived += BrightnessClient_NotifyBrightnessReceived;
        }
    }
}
