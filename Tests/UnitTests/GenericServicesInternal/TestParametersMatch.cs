﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataLayer.EfCode;
using GenericServices.Unity.Configuration;
using GenericServices.Unity.Internal.Decoders;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Tests.UnitTests.GenericServicesInternal
{
    public class TestParametersMatch
    {
#pragma warning disable xUnit1013 // Public method should be marked as test
        public void ParemeterlessMethod() { }
        public void SetMyInt(int myInt) { }

        public void SetMyString(string myString) { }

        public void NotMatch(string myString, int notMyInt) { }

        public int MyInt { get; set; }
        public string MyString { get; set; }
#pragma warning restore xUnit1013 // Public method should be marked as test

        private static MethodInfo _paremeterlessMethod = typeof(TestParametersMatch).GetMethod(nameof(ParemeterlessMethod));
        private static MethodInfo _setMyInt = typeof(TestParametersMatch).GetMethod(nameof(SetMyInt));
        private static MethodInfo _setMyString = typeof(TestParametersMatch).GetMethod(nameof(SetMyString));
        private static PropertyInfo _myIntProp = typeof(TestParametersMatch).GetProperty(nameof(MyInt));
        private static PropertyInfo _myStringProp = typeof(TestParametersMatch).GetProperty(nameof(MyString));


        [Theory]
        [InlineData(nameof(MyInt), nameof(SetMyInt), 1.0)]
        [InlineData(nameof(MyString), nameof(SetMyInt), 0.0)]
        [InlineData(nameof(MyInt), nameof(ParemeterlessMethod), 1.0)]
        public void TestMatchGood(string propName, string methodName, double expectedScore)
        {
            //SETUP
            var prop = typeof(TestParametersMatch).GetProperty(propName);
            var method = typeof(TestParametersMatch).GetMethod(methodName);

            //ATTEMPT
            var match = new ParametersMatch(method.GetParameters(), new List<PropertyInfo> {prop}, DefaultNameMatcher.MatchCamelAndPascalName);

            //VERIFY
            match.Score.ShouldEqual(expectedScore);
        }

        [Fact]
        public void TestPartialMatch()
        {
            //SETUP
            var props = new List<PropertyInfo> {_myIntProp, _myStringProp};
            var method = typeof(TestParametersMatch).GetMethod(nameof(NotMatch));

            //ATTEMPT
            var match = new ParametersMatch(method.GetParameters(), props, DefaultNameMatcher.MatchCamelAndPascalName);

            //VERIFY
            match.Score.ShouldEqual(0.65);
        }

#pragma warning disable xUnit1013 // Public method should be marked as test
        public void MyMethodWithDb(int myInt, EfCoreContext context) { }
#pragma warning restore xUnit1013 // Public method should be marked as test

        [Fact]
        public void TesDbContextMatch()
        {
            //SETUP
            var props = new List<PropertyInfo> { _myIntProp, _myStringProp };
            var method = typeof(TestParametersMatch).GetMethod(nameof(MyMethodWithDb));
            var matcher = new MethodCtorMatcher(new GenericServicesConfig().NameMatcher);

            //ATTEMPT
            var match = matcher.GradeAllMethods(new[] {method}, props, HowTheyWereAskedFor.Unset).Single();


            //VERIFY
            match.PropertiesMatch.Score.ShouldEqual(1);
            match.PropertiesMatch.MatchedPropertiesInOrder.First().MatchSource.ShouldEqual(MatchSources.Property);
            match.PropertiesMatch.MatchedPropertiesInOrder.Last().MatchSource.ShouldEqual(MatchSources.DbContext);
        }
    }
}