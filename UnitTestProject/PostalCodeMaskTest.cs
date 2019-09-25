using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVC_App.Domain.Models;
using MVC_App.Services;
using MVC_App.Repositories;

namespace UnitTestProject
{
    [TestClass]
    public class PostalCodeMaskTest
    {

        [TestMethod]
        [DynamicData(nameof(ApplyMaskScenario), DynamicDataSourceType.Method)]
        public void Test_ApplyMask(
            string displayName,
            string value,
            string mask,
            string result
        )
        {
            var mockRelation = new Mock<GenericRepository<Relation>>();
            mockRelation.Setup(repo => repo.Get()).Returns(GetTest<Relation>());

            var mockCountry = new Mock<GenericRepository<Country>>();
            mockCountry.Setup(repo => repo.Get()).Returns(GetTest<Country>());

            var mockCategory = new Mock<GenericRepository<Category>>();
            mockCategory.Setup(repo => repo.Get()).Returns(GetTest<Category>());

            var mockRelationCategory = new Mock<GenericRepository<RelationCategory>>();
            mockRelationCategory.Setup(repo => repo.Get()).Returns(GetTest<RelationCategory>());

            var mockRelationAddress = new Mock<GenericRepository<RelationAddress>>();
            mockRelationAddress.Setup(repo => repo.Get()).Returns(GetTest<RelationAddress>());

            IRelationService classUnderTest = new RelationService(new UnitOfWork() {
                RelationRepository = mockRelation.Object,
                CategoryRepository = mockCategory.Object,
                CountryRepository = mockCountry.Object,
                RelationAddressRepository = mockRelationAddress.Object,
                RelationCategoryRepository = mockRelationCategory.Object
            });

            Assert.AreEqual(classUnderTest.ApplyMask(value, mask), result);
        }

        private IEnumerable<T> GetTest<T>()
        {
            return new List<T>();
        }

        private static IEnumerable<object[]> ApplyMaskScenario()
        {

            yield return new object[]
            {
                "Test case 1 : ApplyMask_Given_value_with_the_coincidence_of_mask_Should_return_the_same_value",
                "1488-HH",
                "NNNN-LL",
                "1488-HH"
            };

            yield return new object[]
            {
                "Test case 2 : ApplyMask_Given_value_with_register_differ_from_mask_Should_return_modified_value_with_mask_register_to_upper",
                "1488-hh",
                "NNNN-LL",
                "1488-HH"
            };

            yield return new object[]
            {
                "Test case 3 : ApplyMask_Given_value_with_register_differ_from_mask_Should_return_modified_value_with_mask_register_to_lower",
                "1488-HH",
                "NNNN-ll",
                "1488-hh"
            };

            yield return new object[]
            {
                "Test case 4 : ApplyMask_Given_value_with_char_number_differ_from_mask_Should_return_the_same_value",
                "148888-HH",
                "NNNN-LL",
                "148888-HH"
            };

            yield return new object[]
            {
                "Test case 5 : ApplyMask_Given_value_with_no_separators_Should_return_modified_value_with_mask_separators_applied",
                "1488HH",
                "NN--NN--LL",
                "14--88--HH"
            };

            yield return new object[]
            {
                "Test case 6 : ApplyMask_Given_empty_value_Should_return_empty_value",
                "",
                "NNNN-LL",
                ""
            };

            yield return new object[]
            {
                "Test case 7 : ApplyMask_Given_value_with_no_mask_Should_return_the_same_value",
                "1488-HH",
                "",
                "1488-HH"
            };

            yield return new object[]
            {
                "Test case 8 : ApplyMask_Given_value_with_numbers_instead_of_letters_Should_return_the_same_value",
                "1488-88",
                "NNNN-LL",
                "1488-88"
            };

            yield return new object[]
            {
                "Test case 9 : ApplyMask_Given_value_with_letters_instead_of_numbers_Should_return_the_same_value",
                "14HH-HH",
                "NNNN-LL",
                "14HH-HH"
            };

            yield return new object[]
            {
                "Test case 10 : ApplyMask_Given_value_with_numbers_only_and_mask_with_additional_letters_Should_return_the_same_value",
                "1488",
                "NNNN-LL",
                "1488"
            };
        }
    }
}
