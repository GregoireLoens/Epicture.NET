using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Search;
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
    public sealed partial class ImgurSearch : Page, INotifyPropertyChanged {

        private ObservableCollection<Img> _images { get; } = new ObservableCollection<Img>();
        public event PropertyChangedEventHandler PropertyChanged;
        public double ItemSize {
            get => _itemSize;
            set {
                if (_itemSize != value) {
                    _itemSize = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemSize)));
                }
            }
        }
        private double _itemSize;

        String old_search = "";

        Api.Imgur.Imgur imgur = new Api.Imgur.Imgur();

        public ImgurSearch()
        {
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
            var param = e.Parameter as Tuple<String, Api.AApi.size, Api.AApi.type>;
            var search = param.Item1;

            if (search != old_search)
                _images.Clear();

            if (_images.Count == 0) {
                await GetItemsAsync(search, param.Item2, param.Item3);
            }

            TitleTextBlock.Text = "Imgur Search: \"" + search + "\" Size: " + param.Item2.ToString() + " Type: " + param.Item3.ToString();

            base.OnNavigatedTo(e);
        }

        private async Task GetItemsAsync(String search, Api.AApi.size size, Api.AApi.type type) {
            var img = await imgur.SearchImage(search, size, type);
            if (img == null)
                return;
            foreach (var i in img) {
                _images.Add(i);
            }
        }

        private void ImageGridView_ItemClick(object sender, ItemClickEventArgs e) {
            this.Frame.Navigate(typeof(ImgurDetails), e.ClickedItem);
        }
    }
}
