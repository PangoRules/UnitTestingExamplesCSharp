#region[Usings]
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;
#endregion

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        private Fundamentals.Math _math;

        /// <summary>
        /// Function that setups the object _math to a new instace of Fundamentals.Math() before each test
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _math = new Fundamentals.Math();
        }

        #region[Add method]
        /// <summary>
        /// Function that test add method to return an integer with the sum of two values.
        /// </summary>
        [Test]
        //[Ignore("Because I wanted to")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            //Arange
            //var math = new Fundamentals.Math();

            //Act
            var result = _math.Add(1, 2);

            //Assert
            Assert.That(result, Is.EqualTo(3));
            //Assert.That(_math, Is.Not.Null); WRONG ASSERTION UNTRUSTWORTHY TEST
        }
        #endregion

        #region[Max method]
        /// <summary>
        /// Test function that returns the greater value depending on the two parameters given.
        /// </summary>
        /// <param name="a">Int: Integer to compare to b.</param>
        /// <param name="b">Int: Integer to compare to a.</param>
        /// <param name="expectedResult">Int: Expected result between the comparasions</param>
        [Test]
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 2)]
        public void Max_WhenCalled_ReturnGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        #region[Extra test cases for Max method before TestCase]
        ///// <summary>
        ///// Function that checks two arguments and returns the first argument since it's going to be the greater
        ///// </summary>
        //[Test]
        //public void Max_FirstArgumentGreater_ReturnFirstArgument()
        //{
        //    //Arrange
        //    //var math = new Fundamentals.Math();
        //    //Act
        //    var result = _math.Max(2, 1);
        //    //Assert
        //    Assert.That(result, Is.EqualTo(2));
        //}

        ///// <summary>
        ///// Function that checks two arguments and returns the second argument since it's going to be the greater
        ///// </summary>
        //[Test]
        //public void Max_SecondArgumentGreater_ReturnSecondArgument()
        //{
        //    //Arrange
        //    //var math = new Fundamentals.Math();
        //    //Act
        //    var result = _math.Max(1, 2);
        //    //Assert
        //    Assert.That(result, Is.EqualTo(2));
        //}

        ///// <summary>
        ///// Function that checks two arguments and returns any of the two argument since they're the same
        ///// </summary>
        //[Test]
        //public void Max_TwoArgumentsEqual_ReturnSameArgument()
        //{
        //    //Arrange
        //    //var math = new Fundamentals.Math();
        //    //Act
        //    var result = _math.Max(2, 2);
        //    //Assert
        //    Assert.That(result, Is.EqualTo(2));
        //}
        #endregion
        #endregion

        #region[GetOddNumbers]
        /// <summary>
        /// Test function that returns the list of odd numbers up to limit passed in parameter.
        /// </summary>
        /// <param name="limit">Int: limit to get the list of odd numbers.</param>
        /// <param name="expectedArray">Int []: Expected array result</param>
        [Test]
        [TestCase(5, new[] { 1,3,5 })]
        [TestCase(3, new[] { 1,3 })]
        [TestCase(0, new int[] { } )]
        [TestCase(-3, new int[] { })]
        public void GetOddNumbers_WhenCalled_ReturnOddNumbersUpToLimit(int limit, int[] expectedArray)
        {
            var result = _math.GetOddNumbers(limit);

            Assert.That(result, Is.EquivalentTo(expectedArray));
        }

        #region[Extra test cases for GetOddNumbers method before TestCase]
        /// <summary>
        /// Function that gets list of odd number up to limit from number greater than cero (HAS EXAMPLES OF ASSERT!!)
        /// </summary>
        //[Test]
        //public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        //{
        //    var result = _math.GetOddNumbers(5);

        //    //Assert.That(result, Is.Not.Empty);

        //    //Assert.That(result.Count(), Is.EqualTo(3));

        //    //Assert.That(result, Does.Contain(1));
        //    //Assert.That(result, Does.Contain(3));
        //    //Assert.That(result, Does.Contain(5));

        //    //Assert.That(result, Is.Ordered);

        //    //Assert.That(result, Is.Unique);

        //    Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
        //}

        ///// <summary>
        ///// Function that gets list of odd number up to limit from number lesser than cero.
        ///// </summary>
        //[Test]
        //public void GetOddNumbers_LimitIsLessThanZero_ReturnOddNumbersUpToLimit()
        //{
        //    var result = _math.GetOddNumbers(-5);

        //    Assert.That(result, Is.Empty);
        //}

        ///// <summary>
        ///// Function that gets list of odd number up to limit from number that is cero.
        ///// </summary>
        //[Test]
        //public void GetOddNumbers_LimitIsZero_ReturnOddNumbersUpToLimit()
        //{
        //    var result = _math.GetOddNumbers(0);

        //    Assert.That(result, Is.Empty);
        //}
        #endregion
        #endregion
    }
}
