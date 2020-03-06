using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace let_me_use_my_accent_colors
{
    public static class Ext
    {
        public static bool Contains(this List<FirstPartyApp> list, string name)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].name == name)
                    return true;
            }

            return false;
        }
    }
}
