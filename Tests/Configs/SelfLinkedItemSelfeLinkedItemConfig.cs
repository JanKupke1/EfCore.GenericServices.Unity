// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tests.EfClasses;

namespace Tests.Configs
{
    public class SelfLinkedItemSelfeLinkedItemConfig : IEntityTypeConfiguration<SelfLinkedItemSelfeLinkedItem>
    {
        public void Configure
            (EntityTypeBuilder<SelfLinkedItemSelfeLinkedItem> entity)
        {
            entity.HasKey(p =>
                new { p.SelfLinkedItemChildId, p.SelfLinkedItemParentId }); //#A

            //-----------------------------
            //Relationships

            entity.HasOne(pt => pt.SelfLinkedItemParent)        //#C
                .WithMany(p => p.SelfLinkedChildren)   //#C
                .HasForeignKey(pt => pt.SelfLinkedItemParentId);//#C

            entity.HasOne(pt => pt.SelfLinkedItemChild)        //#C
                .WithMany(t => t.SelfLinkedParents)       //#C
                .HasForeignKey(pt => pt.SelfLinkedItemChildId);//#C
        }

        /*Primary key settings**********************************************
        #A Here I use an anonymous object to define two (or more) properties to form a composite key. The order in which the properties appear in the anonymous object defines their order
        * ******************************************************/
        /*******************************************************
        #A This uses the names of the Book and Author primary keys to form its own, composite key
        #B I configure the one-to-many relationship from the Book to BookAuthor entity class
        #C ... and I then configure the other one-to-many relationship from the Author to the BookAuthor entity class
         * ****************************************************/
    }
}