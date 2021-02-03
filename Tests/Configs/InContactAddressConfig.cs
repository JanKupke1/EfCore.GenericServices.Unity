﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System;
using AutoMapper;
using GenericServices.Unity.Configuration;
using Tests.Dtos;
using Tests.EfClasses;

namespace Tests.Configs
{
    public class InContactAddressConfig : PerDtoConfig<InContactAddressDto, ContactAddress>
    {
        //See http://docs.automapper.org/en/stable/Reverse-Mapping-and-Unflattening.html#customizing-reverse-mapping on reverse mapping
        //my test showed that I didn't need the .ReverseMap() - that's most likely because I use AutoMapper's Map, rather that MapFrom
        public override Action<IMappingExpression<InContactAddressDto, ContactAddress>> AlterSaveMapping
        {
            get
            {
                return cfg => cfg.ForMember(d => d.Address, opt => opt.MapFrom(src => src.Addess));
            }
        }
    }
}