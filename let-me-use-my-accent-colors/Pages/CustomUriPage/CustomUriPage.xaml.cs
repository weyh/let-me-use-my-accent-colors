using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace let_me_use_my_accent_colors
{
    public sealed partial class CustomUriPage : Page
    {
        private Dictionary<string, StorageFile> imgs = new Dictionary<string, StorageFile>();

        public CustomUriPage()
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            currentView.BackRequested += (s, e) =>
            {
                if (this.Frame.CanGoBack)
                    this.Frame.GoBack();
            };

            this.InitializeComponent();

            DefStoryboard.Begin();
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
                if(imgs.ContainsKey(((Button)sender).Name.Replace("_Button", "")))
                    imgs.Remove(((Button)sender).Name.Replace("_Button", ""));

                imgs.Add(((Button)sender).Name.Replace("_Button", ""), file);
                ((TextBox)FindName(((Button)sender).Name.Replace("Button", "TextBox"))).Text = file.Path;
            }
            else
                Debug.WriteLine("Img error");
        }

        private async void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Added_Popup.IsOpen = true;

            Stopwatch sw = new Stopwatch();
            EnterStoryboard.Begin();
            sw.Start();

           CApp ca = new CApp(DisplayName_TextBox.Text, CustomUri_TextBox.Text, false);

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            foreach (var img in imgs)
            {
                await img.Value.CopyAsync(localFolder, $"{ca.name}_{img.Key}.png", NameCollisionOption.ReplaceExisting);
            }

            CStart.AddCustomApps(ca);
            sw.Stop();
            Debug.WriteLine("DONE" + localFolder.Path);

            if(sw.ElapsedMilliseconds < 2500)
                await Task.Delay(2500 - (int)sw.ElapsedMilliseconds);

            ExitStoryboard.Begin();
            await Task.Delay(1000);

            Added_Popup.IsOpen = false;
        }
    }
}
