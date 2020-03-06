using System;

namespace let_me_use_my_accent_colors
{
    public struct FirstPartyApp
    {
        public string name;
        public string appURI;

        public static Uri GetWide310x150(string name, bool useLegacy)
        {
            if (useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/w.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/wn.png");
        }

        public static Uri GetSquare150x150(string name, bool useLegacy)
        {
            if(useLegacy)
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/s.png");
            else
                return new Uri($"ms-appx:///Resources/Imgs/{name.Replace("&", "And")}/sn.png");
        }

        public static Uri GetSquare310x310(string name, bool useLegacy) => GetSquare150x150(name, useLegacy);
        public static Uri GetSquare71x71(string name, bool useLegacy) => GetSquare150x150(name, useLegacy);
        public static Uri GetSquare44x44(string name, bool useLegacy) => GetSquare150x150(name, useLegacy);

        public FirstPartyApp(string name, string appURI)
        {
            this.name = name;
            this.appURI = appURI;
        }
    }
}
