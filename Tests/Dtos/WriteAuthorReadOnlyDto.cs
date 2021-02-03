﻿// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Tests.Dtos
{
    public class WriteAuthorReadOnlyDto
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        [ReadOnly(true)]
        public string Email { get; set; }
    }
}