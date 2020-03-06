using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.System;
using Windows.UI.StartScreen;

namespace let_me_use_my_accent_colors
{
    class CStart
    {
        public static List<FirstPartyApp> firstPartyApps;
        public static List<SecondaryTile> secondaryTiles = new List<SecondaryTile>();

        public async static void Applicion(string s)
        {
            if (firstPartyApps.Contains(s))
            {
                await Launcher.LaunchUriAsync(new Uri(firstPartyApps.Find(x => x.name == s).appURI));
                Windows.UI.Xaml.Application.Current.Exit();
            }
            else
            {
                Debug.Fail($"FirstPartyAppURIs.ContainsKey({s})");
            }
        }

        public async static void LoadFirstPartyAppURIs()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/uri_dict.txt"));
            var dict = await FileIO.ReadLinesAsync(file);

            firstPartyApps = dict.Select(x => new FirstPartyApp(x.Split(',')[0], x.Split(',')[1])).ToList();
        }

        public async static void LoadSavedTiles()
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalCacheFolder.GetFileAsync("tiles.json");
                string json = await FileIO.ReadTextAsync(file);

                secondaryTiles = JsonConvert.DeserializeObject<List<SecondaryTile>>(json);
            }
            catch
            {
                Debug.Fail("Read fialed");
            }
        }

        public async static void SaveTile(SecondaryTile tile)
        {
            secondaryTiles.Add(tile);
            string json = JsonConvert.SerializeObject(secondaryTiles, Formatting.None);

            StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync("tiles.json",
                CreationCollisionOption.ReplaceExisting);

            _ = FileIO.WriteTextAsync(file, json);
        }

        public async static void SuspendAsync()
        {
            IList<AppDiagnosticInfo> infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
            IList<AppResourceGroupInfo> resourceInfos = infos[0].GetResourceGroups();
            await resourceInfos[0].StartSuspendAsync();
        }

        public async static void ResumeAsync()
        {
            IList<AppDiagnosticInfo> infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
            IList<AppResourceGroupInfo> resourceInfos = infos[0].GetResourceGroups();
            await resourceInfos[0].StartResumeAsync();
        }
    }
}
