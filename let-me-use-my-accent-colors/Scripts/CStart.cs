using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Storage;
using Windows.System;

namespace let_me_use_my_accent_colors
{
    class CStart
    {
        public static List<CApp> cApps { get; private set; }

        /// <summary>
        /// Starts app
        /// </summary>
        /// <param name="uriName">The name associated with the uri</param>
        public async static void Applicion(string uriName)
        {
            if (cApps.Contains(uriName))
                await Launcher.LaunchUriAsync(new Uri(cApps.Find(x => x.name == uriName).appURI));
            else
                Debug.WriteLine($"FirstPartyAppURIs.ContainsKey({uriName})");
        }

        /// <summary>
        /// Loads uris from uri_dict.txt
        /// </summary>
        public async static void LoadFirstPartyAppURIs()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Resources/uri_dict.txt"));
            var dict = await FileIO.ReadLinesAsync(file);

            cApps = dict.Select(x => new CApp(x.Split(',')[0], x.Split(',')[1])).ToList();
        }

        public async static void LoadCustomApps()
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("customApps.json");
                string json = await FileIO.ReadTextAsync(file);

                cApps.AddRange(JsonConvert.DeserializeObject<List<CApp>>(json));
            }
            catch
            {
                Debug.WriteLine("Read fialed");
            }
        }

        public static void AddCustomApps(CApp customApp)
        {
            cApps.Add(customApp);
            SaveCustomApps();
        }

        public static void RemoveCustomApps(CApp customApp)
        {
            cApps.Remove(customApp);
            SaveCustomApps();
        }

        public static void EditCustomApps(CApp customApp)
        {
            int i = cApps.IndexOf(cApps.FindByName(customApp.name));

            if (cApps[i].appURI == customApp.appURI) // only the appUri can be changed
                return;

            cApps[i] = customApp;
            SaveCustomApps();
        }

        public async static void SaveCustomApps()
        {
            string json = JsonConvert.SerializeObject(cApps.Where(x => !x.firstPartyApp), Formatting.None);

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("customApps.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);
        }

        public async static void SuspendAsync()
        {
            IList<AppDiagnosticInfo> infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
            IList<AppResourceGroupInfo> resourceInfos = infos[0].GetResourceGroups();
            await resourceInfos[0].StartSuspendAsync();
        }

        //public async static void ResumeAsync()
        //{
        //    IList<AppDiagnosticInfo> infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
        //    IList<AppResourceGroupInfo> resourceInfos = infos[0].GetResourceGroups();
        //    await resourceInfos[0].StartResumeAsync();
        //}
    }
}
