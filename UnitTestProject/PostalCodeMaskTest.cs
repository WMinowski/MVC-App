using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_App.Controllers;

namespace UnitTestProject
{
    [TestClass]
    public class PostalCodeMaskTest
    {
        RelationsController relationController = new RelationsController();

        [TestMethod]
        public void ValueMaskCoincidence()
        {
            Assert.AreEqual(relationController.ApplyMask("1488-HH", "NNNN-LL"), "1488-HH");
        }

        [TestMethod]
        public void DifferentRegister()
        {
            Assert.AreEqual(relationController.ApplyMask("1488-hh", "NNNN-LL"), "1488-HH");
            Assert.AreEqual(relationController.ApplyMask("1488-HH", "NNNN-ll"), "1488-hh");
        }

        [TestMethod]
        public void DifferentCharNumber()
        {
            Assert.AreEqual(relationController.ApplyMask("148888-HH", "NNNN-LL"), "148888-HH");
        }

        [TestMethod]
        public void NoSeparators()
        {
            Assert.AreEqual(relationController.ApplyMask("1488HH", "NNNN-LL"), "1488-HH");
        }

        [TestMethod]
        public void ManySeparators()
        {
            Assert.AreEqual(relationController.ApplyMask("1488HH", "NN--NN-LL"), "14--88-HH");
        }

        [TestMethod]
        public void EmptyValue()
        {
            Assert.AreEqual(relationController.ApplyMask("", "NNNN-LL"), "");
        }

        [TestMethod]
        public void DifferentChar()
        {
            Assert.AreEqual(relationController.ApplyMask("1488-88", "NNNN-LL"), "1488-88");
            Assert.AreEqual(relationController.ApplyMask("14HH-HH", "NNNN-LL"), "14HH-HH");
        }
    }
}
