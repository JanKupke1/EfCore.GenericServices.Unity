﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using GenericServices.Unity;
using Tests.EfClasses;

namespace Tests.Dtos
{
    public class ChildDbQueryDto : ILinkToEntity<ChildReadOnly>
    {
        public int ChildId { get; set; }
        public string MyString { get; set; }
    }
}