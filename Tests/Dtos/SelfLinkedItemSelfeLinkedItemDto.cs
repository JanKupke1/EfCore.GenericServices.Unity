// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using GenericServices.Unity;
using Tests.EfClasses;

namespace Tests.Dtos
{
    public class SelfLinkedItemSelfeLinkedItemDto : ILinkToEntity<SelfLinkedItemSelfeLinkedItem>
    {
        public int SelfLinkedItemChildId { get; set; }
        public SelfLinkedItemDto SelfLinkedItemChild { get; set; }

        public int SelfLinkedItemParentId { get; set; }
        public SelfLinkedItemDto SelfLinkedItemParent { get; set; }
    }
}