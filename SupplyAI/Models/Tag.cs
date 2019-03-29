using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI3.Models
{
    public class Tag
    {
        public string Name;
        public ulong popularity; //how many deatasets use this tag
        public Tag Parent; //null if top level tag.  many Tags will have more specific subtags associated with them.


        public Tag(string name, Tag parent = null) {
            Name = name.ToLower();
            popularity = 0;
            Parent = parent;
        }


        override public string ToString() {
            if (Parent == null)
                return Name;
            return Parent.ToString() + ":" + Name;
        }
    }
}