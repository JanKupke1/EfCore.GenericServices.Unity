// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using GenericServices.Unity;
using System.Collections.Generic;
using Tests.EfClasses;

namespace Tests.Dtos
{
    public class SelfLinkedItemDto : ILinkToEntity<SelfLinkedItem>
    {
        public string Name { get; set; }

        public int SelfLinkedItemId { get; set; }

        public List<SelfLinkedItemSelfeLinkedItemDto> SelfLinkedChildren { get; set; } = new List<SelfLinkedItemSelfeLinkedItemDto>();
        public List<SelfLinkedItemSelfeLinkedItemDto> SelfLinkedParents { get; set; } = new List<SelfLinkedItemSelfeLinkedItemDto>();
    }
}