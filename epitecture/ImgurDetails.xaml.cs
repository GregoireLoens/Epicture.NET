﻿using System;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace epitecture
{
    public sealed partial class ImgurDetails : Page {

        private Img image;
        Api.Imgur.Imgur imgur = new Api.Imgur.Imgur();

        public ImgurDetails() {
            this.InitializeComponent();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e) {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;
            if (rootFrame.CanGoBack && e.Handled == false) {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e) {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            image = e.Parameter as Img;
            if (image.favorite)
                Fav.Content = "Unfav";
            base.OnNavigatedTo(e);
        }

        private void Fav_Click(object sender, RoutedEventArgs e) {
            if (image.favorite) {
                Fav.Content = "Fav";
                image.favorite = false;
                imgur.Fav(image.id);
            }
            else {
                Fav.Content = "Unfav";
                image.favorite = true;
                imgur.Fav(image.id);
            }
        }
    }
}
