using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace let_me_use_my_accent_colors
{
    public sealed partial class MainPage : Page
    {
        public string[] arrayOfCApps => CStart.cApps.Select(x => x.name).ToArray();
        public string[] tileSizes = new[] { "Default (150x150)", "Wide (310x150)" }; // def * 1.35

        public MainPage()
        {
            App.dontExit = true;
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);
        }

        public async void PinSecondaryTile()
        {
            if (!IsFormValid())
                return;

            string tileId = Package.Current.DisplayName + DateTime.UtcNow.Ticks;
            string displayName = DisplayName_TextBox.Text;

            string uriName = URI_ComboBox.Text;
            string arguments = $"app={uriName}";

            var tileSize = TileSize_ComboBox.Text == "Default (150x150)" ? TileSize.Default : TileSize.Wide310x150;
            bool useLegacyIcons = (bool)UseLegacyIcons.IsChecked;

            SecondaryTile tile = new SecondaryTile(tileId, displayName, arguments,
                CStart.cApps.FindByName(uriName).GetSquare150x150(useLegacyIcons), tileSize);

            tile.VisualElements.Wide310x150Logo = CStart.cApps.FindByName(uriName).GetWide310x150(useLegacyIcons);
            tile.VisualElements.Square310x310Logo = CStart.cApps.FindByName(uriName).GetSquare310x310(useLegacyIcons);
            tile.VisualElements.Square71x71Logo = CStart.cApps.FindByName(uriName).GetSquare71x71(useLegacyIcons);
            tile.VisualElements.Square44x44Logo = CStart.cApps.FindByName(uriName).GetSquare44x44(useLegacyIcons);

            tile.VisualElements.ShowNameOnSquare150x150Logo = (bool)Show150x150_CheckBox.IsChecked;
            tile.VisualElements.ShowNameOnWide310x150Logo = (bool)Show310x150_CheckBox.IsChecked;
            tile.VisualElements.ShowNameOnSquare310x310Logo = (bool)Show310x310_CheckBox.IsChecked;

            if (!SecondaryTile.Exists(tileId))
                await tile.RequestCreateAsync();
        }

        private bool IsFormValid()
        {
            DisplayName_TextBox_LostFocus(null, null);
            URI_ComboBox_LostFocus(null, null);

            return TextBoxRegex.GetIsValid(DisplayName_TextBox) && TileSize_ComboBox.Text != "";
        }

        private void DisplayName_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TextBoxRegex.GetIsValid(DisplayName_TextBox))
                DisplayName_TextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            else
                DisplayName_TextBox.ClearValue(BorderBrushProperty);
        }

        private void URI_ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (URI_ComboBox.Text == "")
                URI_ComboBox.BorderBrush = new SolidColorBrush(Colors.Red);
            else
                URI_ComboBox.ClearValue(BorderBrushProperty);
        }

        private void Create(object sender, RoutedEventArgs e) => PinSecondaryTile();

        private void CreateCustomUri(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(CustomUriListViewPage));
    }
}
