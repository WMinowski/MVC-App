using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_App.Controllers;

namespace UnitTestProject
{
    [TestClass]
    public class PostalCodeMaskTest
    {
        RelationsController relationController = new RelationsController();

        [TestMethod]
        [DynamicData(nameof(ApplyMaskScenario), DynamicDataSourceType.Method)]
        public void Test_ApplyMask(
            string displayName,
            string value,
            string mask,
            string result
        )
        {
            var classUnderTest = new RelationsController();

            Assert.AreEqual(classUnderTest.ApplyMask(value, mask), result);
        }

        private static IEnumerable<object[]> ApplyMaskScenario()
        {

            yield return new object[]
            {
                "Test case 1 : ValueMaskCoincidence",
                "1488-HH",
                "NNNN-LL",
                "1488-HH"
            };

            yield return new object[]
            {
                "Test case 2 : DifferentRegisterLowerToUpper",
                "1488-hh",
                "NNNN-LL",
                "1488-HH"
            };

            yield return new object[]
            {
                "Test case 3 : DifferentRegisterUpperToLower",
                "1488-HH",
                "NNNN-ll",
                "1488-hh"
            };

            yield return new object[]
            {
                "Test case 4 : DifferentCharNumber",
                "148888-HH",
                "NNNN-LL",
                "148888-HH"
            };

            yield return new object[]
            {
                "Test case 5 : NoSeparators",
                "1488HH",
                "NNNN-LL",
                "1488-HH"
            };

            yield return new object[]
            {
                "Test case 6 : ManySeparators",
                "1488HH",
                "NN--NN--LL",
                "14--88--HH"
            };

            yield return new object[]
            {
                "Test case 7 : EmptyValue",
                "",
                "NNNN-LL",
                ""
            };

            yield return new object[]
            {
                "Test case 8 : EmptyMask",
                "1488-HH",
                "",
                "1488-HH"
            };

            yield return new object[]
            {
                "Test case 9 : DifferentCharNumberToLetter",
                "1488-88",
                "NNNN-LL",
                "1488-88"
            };

            yield return new object[]
            {
                "Test case 10 : DifferentCharLetterToNumber",
                "14HH-HH",
                "NNNN-LL",
                "14HH-HH"
            };
        }
    }
}
