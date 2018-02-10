using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace epitecture
{
    public sealed partial class MainPage : Page{

        public MainPage() {
            this.InitializeComponent();
        }

        private void LocalButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Local));
        }

        private void ImgurButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            this.Frame.Navigate(typeof(Imgur));
        }
    }
}
