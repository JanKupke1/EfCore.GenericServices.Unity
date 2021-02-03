﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using GenericServices.Unity.Configuration;
using System;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Tests.UnitTests.GenericServicesPublic
{
    public class TestGenericServicesConfig
    {
        private readonly ITestOutputHelper _output;

        public TestGenericServicesConfig(ITestOutputHelper output)
        {
            _output = output;
        }

        public int MyInt { get; set; }
        private PropertyInfo MyIntProp => GetType().GetProperty(nameof(MyInt));

        [Theory]
        [InlineData("MyInt", 1.0)]
        [InlineData("myInt", 1.0)]
        [InlineData("myint", 0.3)]
        public void TestDifferntNamesOnDefaultNameMatcher(string name, double score)
        {
            //SETUP
            var globalConfig = new GenericServicesConfig();

            //ATTEMPT
            var result = globalConfig.NameMatcher(name, typeof(int), MyIntProp);

            //VERIFY
            Math.Abs( result.Score - score).ShouldBeInRange(0, 0.001);
            _output.WriteLine(result.ToString());
        }

        private PropertyMatch ForceLeadingUnderscore(string name, Type type, PropertyInfo propertyInfo)
        {
            return new PropertyMatch(name.Length > 1 && name[0] == '_' &&
                              name.Substring(1).Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase),
                PropertyMatch.TypeMatchLevels.Match, propertyInfo);
        }

        [Theory]
        [InlineData("_MyInt", 1.0)]
        [InlineData("_myInt", 1.0)]
        [InlineData("myInt", 0.3)]
        public void TestDifferntNamesOnNewNameMatcher(string name, double score)
        {
            //SETUP
            var globalConfig = new GenericServicesConfig
            {
                NameMatcher = ForceLeadingUnderscore
            };

            //ATTEMPT
            var result = globalConfig.NameMatcher(name, typeof(int), MyIntProp);

            //VERIFY
            Math.Abs(result.Score - score).ShouldBeInRange(0, 0.001);
            _output.WriteLine(result.ToString());
        }
    }
}