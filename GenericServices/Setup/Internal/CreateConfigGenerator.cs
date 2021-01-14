// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Reflection;
using AutoMapper;
using GenericServices.Configuration;
using GenericServices.Internal.Decoders;
using GenericServices.Internal.MappingCode;
using Microsoft.EntityFrameworkCore;

namespace GenericServices.Setup.Internal
{
    internal class CreateConfigGenerator
    {
        public dynamic Accessor { get; }

        public CreateConfigGenerator(Type dtoType, DecodedEntityClass entityInfo, object configInfo)
        {
            var myGeneric = typeof(ConfigGenerator<,>);
            var copierType = myGeneric.MakeGenericType(dtoType, entityInfo.EntityType);
            Accessor = Activator.CreateInstance(copierType, new object[] { entityInfo, configInfo });
        }

        public class ConfigGenerator<TDto, TEntity>
     where TDto : class
     where TEntity : class
        {
            private readonly DecodedEntityClass _EntityInfo;
            private readonly PerDtoConfig<TDto, TEntity> _config;


            public ConfigGenerator(DecodedEntityClass entityInfo, PerDtoConfig<TDto, TEntity> config)
            {
                _EntityInfo = entityInfo;
                _config = config;
            }

            public void AddReadMappingToProfile(Profile readProfile)
            {
                if (_config?.AlterReadMapping == null)
                    readProfile.CreateMap<TEntity, TDto>();
                else
                {
                    _config.AlterReadMapping(readProfile.CreateMap<TEntity, TDto>());
                }
            }

            public void AddSaveMappingToProfile(Profile writeProfile)
            {
                if (_config?.AlterSaveMapping == null)
                {
                    var map = writeProfile.CreateMap<TDto, TEntity>();
                    map.ConstructUsing((TDto arg, ResolutionContext resolutionContext) =>
                    {
                        return CreateOrResove(arg, resolutionContext);
                    });

                    map.IgnoreAllPropertiesWithAnInaccessibleSetter();
                }
                else
                {
                    _config.AlterSaveMapping(writeProfile.CreateMap<TDto, TEntity>().IgnoreAllPropertiesWithAnInaccessibleSetter());
                }
            }

            private TEntity CreateOrResove(TDto dto, ResolutionContext resolutionContext)
            {
                DbContext dbContext = (DbContext)resolutionContext.Options.Items[MapProperties.DbContext];
                if (dbContext != null && AllIdsSet(dto, _EntityInfo.PrimaryKeyProperties))
                {
                    return dbContext.Find<TEntity>(GetAllValues(dto, _EntityInfo.PrimaryKeyProperties));
                }
                else
                {
                    return Activator.CreateInstance<TEntity>();
                }
            }

            private object[] GetAllValues(TDto dto, ImmutableList<PropertyInfo> primaryKeyProperties)
            {
                object[] result = new object[primaryKeyProperties.Count];

                var type = dto.GetType();
                for (int i = 0; i < primaryKeyProperties.Count; i++)
                {
                    try
                    {
                        var keyProperty = type.GetProperty(primaryKeyProperties[i].Name, BindingFlags.Public | BindingFlags.Instance);
                        result[i] = keyProperty.GetValue(dto);
                    }
                    catch (AmbiguousMatchException)
                    {

                    }
                }

                return result;
            }

            private bool AllIdsSet(TDto dto, ImmutableList<PropertyInfo> primaryKeyProperties)
            {
                foreach (var item in primaryKeyProperties)
                {

                    var type = dto.GetType();
                    object value = null;
                    try
                    {
                        var keyProperty = type.GetProperty(item.Name, BindingFlags.Public | BindingFlags.Instance);
                        value =  keyProperty?.GetValue(dto)  ;
                    }
                    catch (AmbiguousMatchException)
                    {
                        return false;
                    }

                    if (value is null) return false;
                    string sValue = value.ToString();
                    if (string.IsNullOrEmpty(sValue)) return false;

                    if (int.TryParse(sValue, out int intValue))
                    {
                        if (intValue == 0) return false;
                    }
                }
                return true;
            }
        }
    }
}