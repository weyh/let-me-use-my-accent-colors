using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.StartScreen;
using Windows.ApplicationModel;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace let_me_use_my_accent_colors
{
    public sealed partial class MainPage : Page
    {
        public string[] arrayOfFirstPartyAppNames = CStart.firstPartyApps.Select(x => x.name).ToArray();
        public TileSize[] tileSizes = Enum.GetValues(typeof(TileSize)).OfType<TileSize>()
            .Where(x => x.ToString() != "Square30x30" && x.ToString() != "Square70x70").ToArray();

        public MainPage() => this.InitializeComponent();

        public async void PinSecondaryTile()
        {
            bool useLegacyIcons = (bool)((CheckBox)FindName("UseLegacyIcons")).IsChecked;

            string tileId = Package.Current.DisplayName + DateTime.UtcNow.Ticks;

            string displayName = ((TextBox)FindName("DisplayName_TextBox")).Text;
            string arguments = $"app={((ComboBox)FindName("URI_ComboBox")).Text}";

            var tileSize = (TileSize)Enum.Parse(typeof(TileSize), ((ComboBox)FindName("TileSize_ComboBox")).Text);

            SecondaryTile tile = new SecondaryTile(tileId, displayName, arguments,
                FirstPartyApp.GetSquare150x150(displayName, useLegacyIcons), tileSize);

            tile.VisualElements.Wide310x150Logo = FirstPartyApp.GetWide310x150(displayName, useLegacyIcons);
            tile.VisualElements.Square310x310Logo = FirstPartyApp.GetSquare310x310(displayName, useLegacyIcons);
            tile.VisualElements.Square71x71Logo = FirstPartyApp.GetSquare71x71(displayName, useLegacyIcons);
            tile.VisualElements.Square44x44Logo = FirstPartyApp.GetSquare44x44(displayName, useLegacyIcons);
            tile.VisualElements.Square150x150Logo = FirstPartyApp.GetSquare150x150(displayName, useLegacyIcons);

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
