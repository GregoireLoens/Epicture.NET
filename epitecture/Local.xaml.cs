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
    public sealed partial class Local : Page, INotifyPropertyChanged {

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

        public Local()
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

            base.OnNavigatedTo(e);
        }

        private async Task GetItemsAsync() {
            QueryOptions queryOption = new QueryOptions(CommonFileQuery.OrderByTitle, new string[] { ".png", ".jpg", ".jpeg" });
            queryOption.FolderDepth = FolderDepth.Deep;
            Queue<IStorageFolder> folders = new Queue<IStorageFolder>();
            var files = await KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(queryOption).GetFilesAsync();


            foreach (StorageFile file in files) {
                var img = new Img();
                img.data = new BitmapImage();

                using (IRandomAccessStream fileStream = await file.OpenReadAsync()) {
                    img.data.SetSource(fileStream);
                }
                img.id = "";
                img.title = file.DisplayName;
                img.width = img.data.PixelWidth.ToString();
                img.height = img.data.PixelHeight.ToString();
                img.link = file.Path;
                img.file = file;
                _images.Add(img);
            }
        }

        private void ImageGridView_ItemClick(object sender, ItemClickEventArgs e) {
            this.Frame.Navigate(typeof(LocalDetails), e.ClickedItem);
        }
    }
}
