using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativeCodingChallenge.Utils;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        static List<long> numbers = new List<long>();

        [TestInitialize]
        public void Setup()
        {
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
        }

        [TestMethod]
        public void TestSumEven()
        {
            var result = Utilities.SumEven(numbers);
            Assert.AreEqual(result,2);
        }

        //[TestMethod]
        public void TestTimeOk()
        {
            var result = Utilities.PrintHttpGET("https://localhost:44329/api/time");
            Assert.AreNotEqual(result, "Simulated Server Error");
        }

        //[TestMethod]
        public void TestTimeNotOk()
        {
            var result = Utilities.PrintHttpGET("https://localhost:44329/api/time?simulatedError=true");
            Assert.AreEqual(result, "Simulated Server Error");
        }

    }
}