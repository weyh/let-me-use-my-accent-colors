using System.Collections.Generic;

namespace let_me_use_my_accent_colors
{
    public static class Ext
    {
        public static bool Contains(this List<CApp> list, string name)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].name == name)
                    return true;
            }

            return false;
        }

        public static CApp FindByName(this List<CApp> list, string name) => list.Find(x => x.name == name);
    }
}
