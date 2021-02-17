// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System.Linq;
using GenericServices.Unity;
using GenericServices.Unity.PublicButHidden;
using GenericServices.Unity.Setup;
using Tests.Dtos;
using Tests.EfClasses;
using Tests.EfCode;
using Tests.Helpers;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Tests.UnitTests.TestIssues
{
    public class TestSelfReferencingDataClass
    {

        [Fact]
        public void TestSelfLinkedItemDto_TestSaveAndUpdateFor4TimesWithService_AlwaysSameChildCount()
        {
            var options = SqliteInMemory.CreateOptions<TestDbContext>();
            using (var context = new TestDbContext(options))
            {
                //SETUP
                context.Database.EnsureCreated();
                var utData = context.SetupSingleDtoAndEntities<SelfLinkedItemDto>();
                utData.AddSingleDto<SelfLinkedItemSelfeLinkedItemDto>();
                var service = new CrudServices(context, utData.ConfigAndMapper);


                context.SeedDatabaseSelfeLinkedItems();



           

              var allSelfeLinkedItems=  service.ReadManyNoTracked<SelfLinkedItemDto>().ToList();

                string rootName = "root";
                var root = new SelfLinkedItemDto { Name = rootName };


                for (int i = 0; i < allSelfeLinkedItems.Count; i++)
                {
                    var linkItem = new SelfLinkedItemSelfeLinkedItemDto { SelfLinkedItemChild = allSelfeLinkedItems[i], SelfLinkedItemParent=root };
                    root.SelfLinkedChildren.Add(linkItem);
                }

                service.CreateAndSave(root);
                //VERIFY
                service.IsValid.ShouldBeTrue();



               var root2= service.ReadSingle<SelfLinkedItemDto>(root.SelfLinkedItemId);
                foreach (var item in root2.SelfLinkedChildren)
                {
                    item.SelfLinkedItemParent = service.ReadSingle<SelfLinkedItemDto>(item.SelfLinkedItemParentId);
                    item.SelfLinkedItemChild = service.ReadSingle<SelfLinkedItemDto>(item.SelfLinkedItemChildId);
                }
                service.UpdateAndSave(root2);
                //VERIFY
                service.IsValid.ShouldBeTrue();


                var root3 = service.ReadSingle<SelfLinkedItemDto>(root.SelfLinkedItemId);
                service.UpdateAndSave(root3);
                //VERIFY
                service.IsValid.ShouldBeTrue();


                var root4 = service.ReadSingle<SelfLinkedItemDto>(root.SelfLinkedItemId);
                service.UpdateAndSave(root4);
                //VERIFY
                service.IsValid.ShouldBeTrue();

                Assert.True( root.SelfLinkedChildren.Count == root2.SelfLinkedChildren.Count &
                                   root.SelfLinkedChildren.Count == root3.SelfLinkedChildren.Count &
                                   root.SelfLinkedChildren.Count == root4.SelfLinkedChildren.Count);


              

            }
        }



        [Fact]
        public void TestSelfLinkedItemDto_TestSaveAndUpdateFor4TimesWithDataContext_AlwaysSameChildCount()
        {
            var options = SqliteInMemory.CreateOptions<TestDbContext>();
            using (var context = new TestDbContext(options))
            {
                //SETUP
                context.Database.EnsureCreated();
                var utData = context.SetupSingleDtoAndEntities<SelfLinkedItemDto>();
                utData.AddSingleDto<SelfLinkedItemSelfeLinkedItemDto>();
                var service = new CrudServices(context, utData.ConfigAndMapper);


                context.SeedDatabaseSelfeLinkedItems();


                var allSelfeLinkedItems = service.ReadManyNoTracked<SelfLinkedItemDto>().ToList();

                string rootName = "root";
                var root = new SelfLinkedItemDto { Name = rootName };


                for (int i = 0; i < allSelfeLinkedItems.Count; i++)
                {
                    var linkItem = new SelfLinkedItemSelfeLinkedItemDto { SelfLinkedItemChild = allSelfeLinkedItems[i], SelfLinkedItemParent = root };
                    root.SelfLinkedChildren.Add(linkItem);
                }

                service.CreateAndSave(root);
                //VERIFY
                service.IsValid.ShouldBeTrue();

                 var root2 = context.SelfeLinkedItems.Find(root.SelfLinkedItemId);
                //foreach (var item in root2.SelfLinkedChildren)
                //{
                //    item.SelfLinkedItemParent = service.ReadSingle<SelfLinkedItemDto>(item.SelfLinkedItemParentId);
                //    item.SelfLinkedItemChild = service.ReadSingle<SelfLinkedItemDto>(item.SelfLinkedItemChildId);
                //}
                service.UpdateAndSave(root2);
                //VERIFY
                service.IsValid.ShouldBeTrue();


                var root3 = context.SelfeLinkedItems.Find(root.SelfLinkedItemId);
                service.UpdateAndSave(root3);
                //VERIFY
                service.IsValid.ShouldBeTrue();


                var root4 = context.SelfeLinkedItems.Find(root.SelfLinkedItemId);
                service.UpdateAndSave(root4);
                //VERIFY
                service.IsValid.ShouldBeTrue();

                Assert.True(root.SelfLinkedChildren.Count == root2.SelfLinkedChildren.Count &
                                   root.SelfLinkedChildren.Count == root3.SelfLinkedChildren.Count &
                                   root.SelfLinkedChildren.Count == root4.SelfLinkedChildren.Count);


            }
        }

    
    }

}