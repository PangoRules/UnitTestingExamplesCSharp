using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using TestNinja.Mocking.Repositories;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    class BookingHelperTests
    {
        private Mock<IBookingRepository> _mockBookingRepository;
        private Booking _existingBooking;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2, 
                ArrivalDate = ArriveOn(2017, 1, 15), 
                DepartureDate = DepartOn(2017, 1, 20), 
                Reference = "XD1" 
            };

            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockBookingRepository.Setup(br => br.GetActiveBookings(1))
            .Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, daysBefore: 2),
                    DepartureDate = Before(_existingBooking.ArrivalDate),
                }, _mockBookingRepository.Object
            );

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInMiddleOfExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.ArrivalDate),
                }, _mockBookingRepository.Object
            );

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                }, _mockBookingRepository.Object
            );

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        
        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesInMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = Before(_existingBooking.DepartureDate),
                }, _mockBookingRepository.Object
            );

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        
        [Test]
        public void OverlappingBookingsExist_BookingStartsInMiddleOfAnExistingBookingButFinishesAfter_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                }, _mockBookingRepository.Object
            );

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        
        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesAfterExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.DepartureDate),
                    DepartureDate = After(_existingBooking.DepartureDate, daysAfter: 2),
                }, _mockBookingRepository.Object
            );

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(
                new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Status = "Cancelled"
                }, _mockBookingRepository.Object
            );

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int daysBefore = 1)
        {
            return dateTime.AddDays(-daysBefore);
        }

        private DateTime After(DateTime dateTime, int daysAfter = 1)
        {
            return dateTime.AddDays(daysAfter);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
