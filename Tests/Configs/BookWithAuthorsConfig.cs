﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System;
using System.Linq;
using AutoMapper;
using DataLayer.EfClasses;
using GenericServices.Unity.Configuration;
using Tests.Dtos;

namespace Tests.Configs
{
    public class BookWithAuthorsConfig : PerDtoConfig<BookWithAuthors, Book>
    {
        public override Action<IMappingExpression<Book, BookWithAuthors>> AlterReadMapping
        {
            get { return cfg => cfg.ForMember(x => x.Authors, 
                x => x.MapFrom(book => book.AuthorsLink.OrderBy(y => y.Order).Select(z => z.Author.Name))); }
        }
    }
}