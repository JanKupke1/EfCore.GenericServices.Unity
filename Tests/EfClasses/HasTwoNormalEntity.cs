// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

namespace Tests.EfClasses
{
    public class HasTwoNormalEntity
    {
        public int Id { get; set; }
        public int NormalEntity1Id { get; set; }
        public NormalEntity NormalEntity1 { get; set; }
        public int NormalEntity2Id { get; set; }
        public NormalEntity NormalEntity2 { get; set; }
    }
}