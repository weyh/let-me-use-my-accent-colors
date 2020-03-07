using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace let_me_use_my_accent_colors
{
    public sealed partial class MainPage : Page
    {
        public string[] arrayOfFirstPartyAppNames = CStart.firstPartyApps.Select(x => x.name).ToArray();
        public string[] tileSizes = new[] { "Default (150x150)", "Wide (310x150)" }; // def * 1.35

        public MainPage()
        {
            App.dontExit = true;
            this.InitializeComponent();
        }

        public async void PinSecondaryTile()
        {
            string tileId = Package.Current.DisplayName + DateTime.UtcNow.Ticks;
            string displayName = ((TextBox)FindName("DisplayName_TextBox")).Text;

            string uriName = ((ComboBox)FindName("URI_ComboBox")).Text;
            string arguments = $"app={uriName}";

            var tileSize = ((ComboBox)FindName("TileSize_ComboBox")).Text == "Default (150x150)" ? TileSize.Default : TileSize.Wide310x150;
            bool useLegacyIcons = (bool)((CheckBox)FindName("UseLegacyIcons")).IsChecked;

            SecondaryTile tile = new SecondaryTile(tileId, displayName, arguments,
                FirstPartyApp.GetSquare150x150(uriName, useLegacyIcons), tileSize);

            tile.VisualElements.Wide310x150Logo = FirstPartyApp.GetWide310x150(uriName, useLegacyIcons);
            tile.VisualElements.Square310x310Logo = FirstPartyApp.GetSquare310x310(uriName, useLegacyIcons);
            tile.VisualElements.Square71x71Logo = FirstPartyApp.GetSquare71x71(uriName, useLegacyIcons);
            tile.VisualElements.Square44x44Logo = FirstPartyApp.GetSquare44x44(uriName, useLegacyIcons);

            tile.VisualElements.ShowNameOnSquare150x150Logo = (bool)((CheckBox)FindName("Show150x150_CheckBox")).IsChecked;
            tile.VisualElements.ShowNameOnWide310x150Logo = (bool)((CheckBox)FindName("Show310x150_CheckBox")).IsChecked;
            tile.VisualElements.ShowNameOnSquare310x310Logo = (bool)((CheckBox)FindName("Show310x310_CheckBox")).IsChecked;

            if (!SecondaryTile.Exists(tileId))
                await tile.RequestCreateAsync();
        }

        /*private async void Browse(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("> Browse_IMG_BTN_Clicked");

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            foreach (string ft in ".bmp;.dib;.rle;.jpg;.jpeg;.jpe;.jfif;.gif;.tif;.tiff;.png".Split(';'))
                picker.FileTypeFilter.Add(ft);

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
                ((TextBox)FindName(((Button)sender).Name.Replace("Button", "TextBox"))).Text = file.Path;
            else
                Debug.WriteLine("Img error");
        }*/

        private void Create(object sender, RoutedEventArgs e) => PinSecondaryTile();
    }
}
