using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.EfClasses
{
    public class SelfLinkedItemSelfeLinkedItem
    {
        public int SelfLinkedItemChildId { get; set; }
        public SelfLinkedItem SelfLinkedItemChild { get; set; }

        public int SelfLinkedItemParentId { get; set; }
        public SelfLinkedItem SelfLinkedItemParent { get; set; }
    }
}
