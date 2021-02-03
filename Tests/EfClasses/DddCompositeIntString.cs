﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

namespace Tests.EfClasses
{
    public class DddCompositeIntString
    { 

        public int MyInt { get; private set; }

        public string MyString { get; private set; }

        private DddCompositeIntString() { }

        public DddCompositeIntString(string myString, int myInt)
        {
            MyString = myString;
            MyInt = myInt;
        }
    }
}