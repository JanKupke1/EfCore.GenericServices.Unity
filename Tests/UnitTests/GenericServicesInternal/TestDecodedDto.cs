﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataLayer.EfClasses;
using DataLayer.EfCode;
using GenericServices.Unity;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using TestSupport.EfHelpers;
using Xunit.Extensions.AssertExtensions;
using GenericServices.Unity.Internal.Decoders;
using GenericServices.Unity.Configuration;

namespace Tests.UnitTests.GenericServicesInternal
{
    public class TestDecodedDto
    {
        private DecodedEntityClass _bookEntityInfo;
        public TestDecodedDto()
        {
            var options = SqliteInMemory.CreateOptions<EfCoreContext>();
            using (var context = new EfCoreContext(options))
            {
                _bookEntityInfo = new DecodedEntityClass(typeof(Book), context);
            }
        }

        public class Dto1 : ILinkToEntity<Book>
        {
            public int BookId { get; set; }

            [ReadOnly(true)]
            public string Title { get; set; }

            public string ImageUrl { get; set; }
        }

        [Fact]
        public void TestDecodedDto1CheckPropertyTypes()
        {
            //SETUP

            //ATTEMPT
            var decoded = new DecodedDto(typeof(Dto1), _bookEntityInfo, new GenericServicesConfig(), null);

            //VERIFY
            decoded.LinkedEntityInfo.EntityType.ShouldEqual(typeof(Book));
            decoded.PropertyInfos.Single(x => x.PropertyType.HasFlag(DtoPropertyTypes.KeyProperty)).PropertyInfo.Name.ShouldEqual(nameof(Dto1.BookId));
            decoded.PropertyInfos.Single(x => x.PropertyType == DtoPropertyTypes.Normal).PropertyInfo.Name.ShouldEqual(nameof(Dto1.ImageUrl));
            var names = decoded.PropertyInfos.Where(x => x.PropertyType.HasFlag(DtoPropertyTypes.ReadOnly)).Select(x => x.PropertyInfo.Name).ToArray();
            names.ShouldEqual(new string[]{ nameof(Dto1.BookId) , nameof(Dto1.Title) });
        }

        //-----------------------------------------------------------
        //DecodedDto methods

        [Fact]
        public void TestFindSetterMethod()
        {
            //SETUP
            var decoded = new DecodedDto(typeof(Tests.Dtos.ChangePubDateDto), _bookEntityInfo, new GenericServicesConfig(), null);

            //ATTEMPT
            var method = decoded.GetMethodToRun(new DecodeName("UpdatePublishedOn"), _bookEntityInfo);

            //VERIFY
            method.Method.Name.ShouldEqual("UpdatePublishedOn");
        }

        [Fact]
        public void TestFindSetterMethodWithGivenNumParams()
        {
            //SETUP
            var decoded = new DecodedDto(typeof(Tests.Dtos.ChangePubDateDto), _bookEntityInfo, new GenericServicesConfig(), null);

            //ATTEMPT
            var method = decoded.GetMethodToRun(new DecodeName("UpdatePublishedOn(1)"), _bookEntityInfo);

            //VERIFY
            method.Method.Name.ShouldEqual("UpdatePublishedOn");
        }

        [Fact]
        public void TestFindSetterMethodBadName()
        {
            //SETUP
            var decoded = new DecodedDto(typeof(Tests.Dtos.ChangePubDateDto), _bookEntityInfo, new GenericServicesConfig(), null);

            //ATTEMPT
            var ex = Assert.Throws<InvalidOperationException>(() => decoded.GetMethodToRun(new DecodeName("badname"), _bookEntityInfo));

            //VERIFY
            ex.Message.ShouldStartWith("Could not find a method of name badname");
        }

        [Fact]
        public void TestFindSetterMethodNumParamsBad()
        {
            //SETUP
            var decoded = new DecodedDto(typeof(Tests.Dtos.ChangePubDateDto), _bookEntityInfo, new GenericServicesConfig(), null);

            //ATTEMPT
            var ex = Assert.Throws<InvalidOperationException>(() => decoded.GetMethodToRun(new DecodeName("UpdatePublishedOn(2)"), _bookEntityInfo));

            //VERIFY
            ex.Message.ShouldStartWith("Could not find a method of name UpdatePublishedOn(2)");
        }

        [Fact]
        public void TestGetDefaultSetterMethod()
        {
            //SETUP
            var decoded = new DecodedDto(typeof(Tests.Dtos.ChangePubDateDto), _bookEntityInfo, new GenericServicesConfig(), null);

            //ATTEMPT
            var method = decoded.GetMethodToRun(new DecodeName(null), _bookEntityInfo);

            //VERIFY
            method.Method.Name.ShouldEqual("UpdatePublishedOn");
        }

        public class ExtraParamsDto : ILinkToEntity<Book>
        {
            [HiddenInput]
            public int BookId { get; set; }

            public string Title { get; set; }

            [DataType(DataType.Date)]
            public DateTime PublishedOn { get; set; }

            public DateTime SomeOtherParam { get; set; }
        }

        [Fact]
        public void TestCheckExtraParamsOk()
        {
            //SETUP
            var decoded = new DecodedDto(typeof(ExtraParamsDto), _bookEntityInfo, new GenericServicesConfig(), null);

            //ATTEMPT
            var method = decoded.GetMethodToRun(new DecodeName(null), _bookEntityInfo);

            //VERIFY
            method.Method.Name.ShouldEqual("UpdatePublishedOn");
        }


    }
}