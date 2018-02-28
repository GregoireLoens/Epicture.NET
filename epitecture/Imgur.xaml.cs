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
    public sealed partial class Imgur : Page, INotifyPropertyChanged {

        private ObservableCollection<Img> _images { get; } = new ObservableCollection<Img>();
        private ObservableCollection<Api.AApi.size> _sizes { get; } = new ObservableCollection<Api.AApi.size> {
            Api.AApi.size.none,
            Api.AApi.size.small,
            Api.AApi.size.med,
            Api.AApi.size.big,
            Api.AApi.size.lrg,
            Api.AApi.size.huge
        };
        private ObservableCollection<Api.AApi.type> _types { get; } = new ObservableCollection<Api.AApi.type>() {
            Api.AApi.type.none,
            Api.AApi.type.jpg,
            Api.AApi.type.png,
            Api.AApi.type.gif,
            Api.AApi.type.anigif,
            Api.AApi.type.album
        };

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

        Api.Imgur.Imgur imgur = new Api.Imgur.Imgur();

        public Imgur()
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

            if (_images.Count == 0) {
                await GetItemsAsync();
            }

            SizeCB.SelectedIndex = 0;
            TypeCB.SelectedIndex = 0;

            base.OnNavigatedTo(e);
        }

        private async Task GetItemsAsync() {
            var img = await imgur.LoadImage();
            if (img == null)
                return;
            foreach (var i in img) {
                _images.Add(i);
            }
        }

        private void ImageGridView_ItemClick(object sender, ItemClickEventArgs e) {
            this.Frame.Navigate(typeof(ImgurDetails), e.ClickedItem);
        }

        private void SearchBt_Click(object sender, RoutedEventArgs e) {
            var param = new Tuple<String, Api.AApi.size, Api.AApi.type>(
                SearchText.Text,
                (Api.AApi.size)SizeCB.SelectedValue,
                (Api.AApi.type)TypeCB.SelectedValue
                );
            this.Frame.Navigate(typeof(ImgurSearch), param);
        }
    }
}
