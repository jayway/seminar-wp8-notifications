using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.System.UserProfile;

namespace Wp8NotificationsDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void OpenLockSettings_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
        }

        private async void UpdateLockScreenImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var isProvider = LockScreenManager.IsProvidedByCurrentApplication;
                if (!isProvider)
                {
                    var op = await LockScreenManager.RequestAccessAsync();

                    isProvider = op == LockScreenRequestResult.Granted;
                }

                if (isProvider)
                {
                    var uri = new Uri("ms-appdata:///Local/Assets/Tiles/kramer.jpg", UriKind.Absolute);
                    LockScreen.SetImageUri(uri);
                }
                else
                {
                    MessageBox.Show("You said no, so I can't update your background.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void UpdateLockScreenCount_Click(object sender, RoutedEventArgs e)
        {
            var tile = ShellTile.ActiveTiles.First();
            var data = new FlipTileData()
            {
                Count = new Random(DateTime.Now.Millisecond).Next(100),
            };
            tile.Update(data);
        }

        private void UpdateLockScreenText_Click(object sender, RoutedEventArgs e)
        {
            var tile = ShellTile.ActiveTiles.First();
            var data = new FlipTileData()
            {
                BackContent = "back content",
                WideBackContent = "wide back content"
            };
            tile.Update(data);
        }

        private void CreateFlipTile_Click(object sender, RoutedEventArgs e)
        {
            var tileData = new FlipTileData()
                               {
                                   Title = "flip tile",
                                   BackTitle = "flip back",
                                   BackContent = "flip content",
                                   WideBackContent = "flip wide content",
                                   Count = 5,
                                   SmallBackgroundImage = new Uri("/Assets/Tiles/george-151.png", UriKind.Relative),
                                   BackgroundImage = new Uri("/Assets/Tiles/george-336.png", UriKind.Relative),
                                   BackBackgroundImage = new Uri("", UriKind.Relative),
                                   WideBackgroundImage = new Uri("/Assets/Tiles/george-wide.png", UriKind.Relative),
                                   WideBackBackgroundImage = new Uri("", UriKind.Relative),
                               };

            ShellTile.Create(new Uri("/MainPage.xaml?tile=flip", UriKind.Relative), tileData, true);

        }

        private void CreateCycleTile_Click(object sender, RoutedEventArgs e)
        {
            var cycleTile = new CycleTileData()
            {
                Title = "cycle tile",
                Count = 6,
                SmallBackgroundImage =
                    new Uri("/Assets/Tiles/george-151.png", UriKind.Relative),
                CycleImages = new Uri[]
                                {
                                    new Uri("/Assets/Tiles/cycle01.jpg",
                                            UriKind.Relative),
                                    new Uri("/Assets/Tiles/cycle02.jpg",
                                            UriKind.Relative),
                                    new Uri("/Assets/Tiles/cycle03.jpg",
                                            UriKind.Relative),
                                    new Uri("/Assets/Tiles/cycle04.jpg",
                                            UriKind.Relative),
                                }
            };
            ShellTile.Create(new Uri("/MainPage.xaml?tile=cycle", UriKind.Relative), cycleTile, true);
        }

        private void CreateIconicTile_Click(object sender, RoutedEventArgs e)
        {

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}