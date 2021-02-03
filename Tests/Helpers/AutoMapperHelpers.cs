﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System;
using AutoMapper;
using GenericServices.Unity.Configuration;
using GenericServices.Unity.PublicButHidden;
using GenericServices.Unity.Setup.Internal;

namespace Tests.Helpers
{
    public static class AutoMapperHelpers
    {

        public static MapperConfiguration CreateReadConfig<TEntity, TDto>(Action<IMappingExpression<TEntity, TDto>> alterMapping)
        {
            var readProfile = new MappingProfile(false);
            alterMapping(readProfile.CreateMap<TEntity, TDto>());
            var readConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(readProfile);
            });
            return readConfig;
        }

        public static WrappedAndMapper CreateWrapperMapper<TDto, TEntity>()
        {
            var readProfile = new MappingProfile(false);
            readProfile.CreateMap<TEntity, TDto>();
            var readConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(readProfile);
            });

            var saveProfile = new MappingProfile(true);
            saveProfile.CreateMap<TDto, TEntity>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            var saveConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(saveProfile);
            });
            return new WrappedAndMapper(new GenericServicesConfig(), readConfig, saveConfig);
        }
    }
}