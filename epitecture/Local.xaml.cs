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

        private ObservableCollection<BitmapImage> _images { get; } = new ObservableCollection<BitmapImage>();
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
            StorageFolder appInstalledFolder = Package.Current.InstalledLocation;
            QueryOptions queryOption = new QueryOptions(CommonFileQuery.OrderByTitle, new string[] { ".png", ".jpg", ".jpeg" });
            queryOption.FolderDepth = FolderDepth.Deep;
            Queue<IStorageFolder> folders = new Queue<IStorageFolder>();
            var files = await KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(queryOption).GetFilesAsync();


            foreach (StorageFile file in files) {
                BitmapImage bitmapImage = new BitmapImage();

                using (IRandomAccessStream fileStream = await file.OpenReadAsync()) {
                    bitmapImage.SetSource(fileStream);
                }
                _images.Add(bitmapImage);
            }
        }
    }
}
