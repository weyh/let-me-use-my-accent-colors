using System;
using System.Diagnostics;
using Windows.Storage;

namespace let_me_use_my_accent_colors
{
    public struct CApp
    {
        public string name;
        public string appURI;

        public bool firstPartyApp;

        public Uri GetWide310x150(bool useLegacy)
        {
            if(!firstPartyApp)
                return new Uri($"ms-appdata:///local/{name}_Wide310x150.png");

            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/Wide310x150Logo.scale-100.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/Wide310x150Logo.scale-100.png");
        }

        public Uri GetSquare150x150(bool useLegacy)
        {
            if (!firstPartyApp)
            {
                return new Uri($"ms-appdata:///local/{name}_Square150x150.png");
            }

            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/Square150x150Logo.scale-100.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/Square150x150Logo.scale-100.png");
        }

        public Uri GetSquare310x310(bool useLegacy)
        {
            if (!firstPartyApp)
                return new Uri($"ms-appdata:///local/{name}_LargeTile.png");

            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/LargeTile.scale-100.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/LargeTile.scale-100.png");
        }

        public Uri GetSquare71x71(bool useLegacy)
        {
            if (!firstPartyApp)
                return new Uri($"ms-appdata:///local/{name}_SmallTile.png");

            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/SmallTile.scale-100.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/SmallTile.scale-100.png");
        }

        public Uri GetSquare44x44(bool useLegacy)
        {
            if (!firstPartyApp)
                return new Uri($"ms-appdata:///local/{name}_Square44x44.png");

            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/Square44x44Logo.targetsize-48.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/Square44x44Logo.targetsize-48.png");
        }

        public CApp(string name, string appURI, bool firstPartyApp = true)
        {
            this.name = name;
            this.appURI = appURI;
            this.firstPartyApp = firstPartyApp;
        }
    }
}
