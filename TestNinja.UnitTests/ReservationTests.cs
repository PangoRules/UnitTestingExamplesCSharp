#region[Usings
using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
#endregion

namespace TestNinja.UnitTests
{   
    #region[MSTests]
    //[TestClass]
    //public class ReservationTests
    //{
    //    /// <summary>
    //    /// Test function that tests CanBeCancelledBy function in Reservation.cs class file. Test the scenario of user being admin and the expected
    //    /// behaviour is to return a true boolean value.
    //    /// </summary>
    //    [TestMethod]
    //    public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
    //    {
    //        //Arrange:
    //        var user = new User { IsAdmin = true };
    //        var reservation = new Reservation();

    //        //Act:
    //        var result = reservation.CanBeCancelledBy(user);

    //        //Assert:
    //        Assert.IsTrue(result);
    //    }

    //    /// <summary>
    //    /// Test function that tests CanBeCancelledBy function in Reservation.cs class file. Test the scenario of user being same user that
    //    /// created the reservation and the expected behaviour is to return a true boolean value.
    //    /// </summary>
    //    [TestMethod]
    //    public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
    //    {
    //        //Arrange
    //        var user = new User();
    //        var reservation = new Reservation { MadeBy = user };

    //        //Act
    //        var result = reservation.CanBeCancelledBy(user);

    //        //Assert
    //        Assert.IsTrue(result);
    //    }

    //    /// <summary>
    //    /// Test function that tests CanBeCancelledBy function in Reservation.cs class file. Test the scenario of user being an incorrect user that
    //    /// wants to cancell the reservation and the expected behaviour is to return a false boolean value.
    //    /// </summary>
    //    [TestMethod]
    //    public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
    //    {
    //        //Arrange:
    //        var correctUser = new User();
    //        var incorrectUser = new User();
    //        var reservation = new Reservation { MadeBy = correctUser };

    //        //Act:
    //        var result = reservation.CanBeCancelledBy(incorrectUser);

    //        //Assert:
    //        Assert.IsFalse(result);
    //    }
    //}
    #endregion

    [TestFixture]
    public class ReservationTests
    {
        /// <summary>
        /// Test function that tests CanBeCancelledBy function in Reservation.cs class file. Test the scenario of user being admin and the expected
        /// behaviour is to return a true boolean value.
        /// </summary>
        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            //Arrange:
            var user = new User { IsAdmin = true };
            var reservation = new Reservation();

            //Act:
            var result = reservation.CanBeCancelledBy(user);

            //Assert:
            Assert.That(result, Is.True);
            //Assert.That(result == true);
            //Assert.IsTrue(result);
        }

        /// <summary>
        /// Test function that tests CanBeCancelledBy function in Reservation.cs class file. Test the scenario of user being same user that
        /// created the reservation and the expected behaviour is to return a true boolean value.
        /// </summary>
        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            //Arrange
            var user = new User();
            var reservation = new Reservation { MadeBy = user };

            //Act
            var result = reservation.CanBeCancelledBy(user);

            //Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Test function that tests CanBeCancelledBy function in Reservation.cs class file. Test the scenario of user being an incorrect user that
        /// wants to cancell the reservation and the expected behaviour is to return a false boolean value.
        /// </summary>
        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            //Arrange:
            var correctUser = new User();
            var incorrectUser = new User();
            var reservation = new Reservation { MadeBy = correctUser };

            //Act:
            var result = reservation.CanBeCancelledBy(incorrectUser);

            //Assert:
            Assert.That(result, Is.False);
        }
    }
}
