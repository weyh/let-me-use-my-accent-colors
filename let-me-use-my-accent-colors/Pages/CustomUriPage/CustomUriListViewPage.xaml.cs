using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace let_me_use_my_accent_colors
{
    public sealed partial class CustomUriListViewPage : Page
    {
        private CApp[] arrayOfCApps => CStart.cApps.Where(x => !x.firstPartyApp).ToArray();

        public CustomUriListViewPage()
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            currentView.BackRequested += (s, e) =>
            {
                if (this.Frame.CanGoBack)
                    this.Frame.GoBack();
            };

            this.InitializeComponent();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string name = ((StackPanel)((Button)sender).Parent).Name;

            if (!CStart.cApps.FindByName(name).firstPartyApp)
                this.Frame.Navigate(typeof(EditCustomUriPage), arrayOfCApps.FindByName(name));
            else
                Debug.WriteLine($"!>> Delete: {name} is firstPartyApp");
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            string name = ((StackPanel)((Button)sender).Parent).Name;


            if (!CStart.cApps.FindByName(name).firstPartyApp)
            {
                CApp app = arrayOfCApps.FindByName(name);
                // tile may also needs to be deleted

                await (await StorageFile.GetFileFromApplicationUriAsync(app.GetWide310x150(false))).DeleteAsync();
                await (await StorageFile.GetFileFromApplicationUriAsync(app.GetSquare150x150(false))).DeleteAsync();
                await (await StorageFile.GetFileFromApplicationUriAsync(app.GetSquare310x310(false))).DeleteAsync();
                await (await StorageFile.GetFileFromApplicationUriAsync(app.GetSquare44x44(false))).DeleteAsync();
                await (await StorageFile.GetFileFromApplicationUriAsync(app.GetSquare71x71(false))).DeleteAsync();

                CStart.RemoveCustomApps(app);
            }

            else
                Debug.WriteLine($"!>> Edit: {name} is firstPartyApp");
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddNewCustomUriPage));
        }
    }
}
