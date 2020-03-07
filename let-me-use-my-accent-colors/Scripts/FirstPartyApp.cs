using System;

namespace let_me_use_my_accent_colors
{
    public struct FirstPartyApp
    {
        public string name;
        public string appURI;

        #region GetIcons
        public static Uri GetWide310x150(string name, bool useLegacy)
        {
            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/Wide310x150Logo.scale-100.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/Wide310x150Logo.scale-100.png");
        }

        public static Uri GetSquare150x150(string name, bool useLegacy)
        {
            if(useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/Square150x150Logo.scale-100.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/Square150x150Logo.scale-100.png");
        }

        public static Uri GetSquare310x310(string name, bool useLegacy)
        {
            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/LargeTile.scale-100.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/LargeTile.scale-100.png");
        }

        public static Uri GetSquare71x71(string name, bool useLegacy)
        {
            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/SmallTile.scale-100.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/SmallTile.scale-100.png");
        }

        public static Uri GetSquare44x44(string name, bool useLegacy)
        {
            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/l/Square44x44Logo.targetsize-48.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/n/Square44x44Logo.targetsize-48.png");
        }
        #endregion

        public FirstPartyApp(string name, string appURI)
        {
            this.name = name;
            this.appURI = appURI;
        }
    }
}
