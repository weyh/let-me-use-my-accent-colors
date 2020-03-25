using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace let_me_use_my_accent_colors
{
    public sealed partial class EditCustomUriPage : Page
    {
        private CApp cApp = new CApp();
        private Dictionary<string, StorageFile> imgs = new Dictionary<string, StorageFile>();

        public EditCustomUriPage()
        {
            this.InitializeComponent();
            DefStoryboard.Begin();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            cApp = (CApp)e.Parameter;

            Wide310x150_TextBox.Text = cApp.GetWide310x150(false).OriginalString;
            Square150x150_TextBox.Text = cApp.GetSquare150x150(false).OriginalString;
            LargeTile_TextBox.Text = cApp.GetSquare310x310(false).OriginalString;
            Square44x44_TextBox.Text = cApp.GetSquare44x44(false).OriginalString;
            SmallTile_TextBox.Text = cApp.GetSquare71x71(false).OriginalString;
        }

        private async void Browse(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("> Browse_IMG_BTN_Clicked");

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                if (imgs.ContainsKey(((Button)sender).Name.Replace("_Button", "")))
                    imgs.Remove(((Button)sender).Name.Replace("_Button", ""));

                imgs.Add(((Button)sender).Name.Replace("_Button", ""), file);
                ((TextBox)FindName(((Button)sender).Name.Replace("Button", "TextBox"))).Text = file.Path;
            }
            else
                Debug.WriteLine("Img error");
        }

        private async void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFormValid())
                return;

            Saved_Popup.VerticalOffset =- ((FrameworkElement)Saved_Popup.Child).ActualHeight / 2;
            Saved_Popup.HorizontalOffset =- ((FrameworkElement)Saved_Popup.Child).ActualWidth / 2;
            Saved_Popup.IsOpen = true;

            Stopwatch sw = new Stopwatch();

            EnterStoryboard.Begin();
            sw.Start();

            CApp ca = new CApp(cApp.name, CustomUri_TextBox.Text, false);

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            foreach (var img in imgs)
                await img.Value.CopyAsync(localFolder, $"{ca.name}_{img.Key}.png", NameCollisionOption.ReplaceExisting);

            CStart.EditCustomApps(ca);
            sw.Stop();

            if (sw.ElapsedMilliseconds < 2500)
                await Task.Delay(2500 - (int)sw.ElapsedMilliseconds);
            await Task.Delay(2500);
            ExitStoryboard.Begin();
            await Task.Delay(1000);

            this.Frame.Navigate(typeof(CustomUriListViewPage));
        }

        private bool IsFormValid()
        {
            CustomUri_TextBox_LostFocus(null, null);

            return TextBoxRegex.GetIsValid(CustomUri_TextBox);
        }

        private void CustomUri_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TextBoxRegex.GetIsValid(CustomUri_TextBox))
                CustomUri_TextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            else
                CustomUri_TextBox.ClearValue(BorderBrushProperty);
        }
    }
}
