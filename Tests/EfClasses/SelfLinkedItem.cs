using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tests.EfClasses
{
    public class SelfLinkedItem
    {
        public const int NameLength = 100;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(NameLength)]
        public string Name { get; set; }

        public int SelfLinkedItemId { get; set; }

        public List<SelfLinkedItemSelfeLinkedItem> SelfLinkedChildren { get; set; }
        public List<SelfLinkedItemSelfeLinkedItem> SelfLinkedParents { get; set; }

    }
}
