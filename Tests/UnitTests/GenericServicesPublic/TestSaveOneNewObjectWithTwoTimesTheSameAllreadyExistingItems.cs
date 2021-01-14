// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System.Linq;
using GenericServices;
using GenericServices.PublicButHidden;
using GenericServices.Setup;
using Tests.Dtos;
using Tests.EfClasses;
using Tests.EfCode;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Tests.UnitTests.TestIssues
{
    public class TestSaveOneNewObjectWithTwoTimesTheSameAllreadyExistingItems
    {

        /// <summary>
        /// ServiceCreateAndSave_TestSaveOneNewObjectWithTwoTimesTheSameAllreadyExistingItems_ThrowsNoExeption
        /// Update entities and avoid 'The instance of entity type 'X' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked.' error
        /// </summary>
        /// <exception cref="System.InvalidOperationException"></exception>
        [Fact]
        public void TestSaveOneNewObjectWithTwoTimesTheSameAllreadyExistingItems_TwoTimesTheSameOne_ThrowsNoExeption()
        {
            var options = SqliteInMemory.CreateOptions<TestDbContext>();
            using (var context = new TestDbContext(options))
            {
                //SETUP
                context.Database.EnsureCreated();
                var utData = context.SetupSingleDtoAndEntities<NormalEntityDto>();
                utData.AddSingleDto<HasTwoNormalEntityDto>();
                var service = new CrudServices(context, utData.ConfigAndMapper);

                var entity = new NormalEntityDto { MyString = "42" };
                service.CreateAndSave(entity);
                //VERIFY
                service.IsValid.ShouldBeTrue();

                //ATTEMPT
                var AllNormals = context.NormalEntities.ToList();
                var fistNormal = AllNormals.First(); //In normal Case, selected by a Combobox

                HasTwoNormalEntity hasTwoNormalEntity = new HasTwoNormalEntity();
                hasTwoNormalEntity.NormalEntity1 = fistNormal;
                hasTwoNormalEntity.NormalEntity2 = fistNormal;

                context.HasTwoNormalEntities.Add(hasTwoNormalEntity);
                var status = context.SaveChangesWithValidation();

                //VERIFY
                status.IsValid.ShouldBeTrue(status.GetAllErrors());

            }
        }

        [Fact]
        public void TestSaveOneNewObjectWithTwoTimesTheSameAllreadyExistingItems_TheItemsAreNotTheSameOneTheIdAreTheSame_NoErrorRaised()
        {
            var options = SqliteInMemory.CreateOptions<TestDbContext>();
            using (var context = new TestDbContext(options))
            {
                //SETUP
                context.Database.EnsureCreated();
                var utData = context.SetupSingleDtoAndEntities<NormalEntityDto>();
                utData.AddSingleDto<HasTwoNormalEntityDto>();
                var service = new CrudServices(context, utData.ConfigAndMapper);

                var entity = new NormalEntityDto { MyString = "42" };
                service.CreateAndSave(entity);
                //VERIFY
                service.IsValid.ShouldBeTrue();

                //ATTEMPT
                var AllNormals = context.NormalEntities.ToList();
                var fistNormal = AllNormals.First(); //In normal Case, selected by a Combobox

                var AllNormals2 = context.NormalEntities.ToList();
                var fistNormal2 = AllNormals2.First(); //In normal Case, selected by a Combobox

                HasTwoNormalEntity hasTwoNormalEntity = new HasTwoNormalEntity();
                hasTwoNormalEntity.NormalEntity1 = fistNormal;
                hasTwoNormalEntity.NormalEntity2 = fistNormal2;

                context.HasTwoNormalEntities.Add(hasTwoNormalEntity);
                var status = context.SaveChangesWithValidation();

                //VERIFY
                status.IsValid.ShouldBeTrue(status.GetAllErrors());

            }
        }

        [Fact]
        public void TestSaveOneNewObjectWithTwoTimesTheSameAllreadyExistingItems_TheItemContentIsNotTheSameOneTheIdAreTheSame_NoErrorRaised()
        {
            var options = SqliteInMemory.CreateOptions<TestDbContext>();
            using (var context = new TestDbContext(options))
            {
                //SETUP
                context.Database.EnsureCreated();
                var utData = context.SetupSingleDtoAndEntities<NormalEntityDto>();
                utData.AddSingleDto<HasTwoNormalEntityDto>();
                var service = new CrudServices(context, utData.ConfigAndMapper);

                var entity = new NormalEntityDto { MyString = "42" };
                service.CreateAndSave(entity);
                //VERIFY
                service.IsValid.ShouldBeTrue();

                //ATTEMPT
                var AllNormals = context.NormalEntities.ToList();
                var fistNormal = AllNormals.First(); //In normal Case, selected by a Combobox
                fistNormal.MyString = "fistNormal1";

                var AllNormals2 = context.NormalEntities.ToList();
                var fistNormal2 = AllNormals2.First(); //In normal Case, selected by a Combobox
                fistNormal2.MyString = "fistNormal2";

                HasTwoNormalEntity hasTwoNormalEntity = new HasTwoNormalEntity();
                hasTwoNormalEntity.NormalEntity1 = fistNormal;
                hasTwoNormalEntity.NormalEntity2 = fistNormal2;

                context.HasTwoNormalEntities.Add(hasTwoNormalEntity);
                var status = context.SaveChangesWithValidation();  //Now, You Don't know, is MyString  fistNormal1 or fistNormal1!!!

                //VERIFY
                status.IsValid.ShouldBeTrue(status.GetAllErrors());

            }
        }
    }

}