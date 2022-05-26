using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetLastErrorProperty()
        {
            var errLogger = new ErrorLogger();

            errLogger.Log("a");

            Assert.That(errLogger.LastError == "a");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            var errLogger = new ErrorLogger();

            Assert.That(() => errLogger.Log(error), Throws.ArgumentNullException);
            //Assert.That(() => errLogger.Log(error), Throws.Exception.TypeOf<DivideByZeroException>);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var errLogger = new ErrorLogger();

            var id = Guid.Empty;

            errLogger.ErrorLogged += (sender, args) => { id = args; };

            errLogger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
