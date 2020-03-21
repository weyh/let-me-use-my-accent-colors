using System.Collections.Generic;
using System.Linq;

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

        public static void RemoveByName(this List<CApp> list, string name) => list.Remove(list.FindByName(name));

        public static CApp FindByName(this List<CApp> list, string name) => list.Find(x => x.name == name);

        public static CApp FindByName(this CApp[] arr, string name) => arr.First(x => x.name == name);
    }
}
