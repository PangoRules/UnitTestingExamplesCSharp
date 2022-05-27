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
    class HousekeeperServiceTests
    {
        #region[Variables]
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IStatementGenerator> _mockStatementGenerator;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<IXtraMessageBox> _mockXtraMessageBox;
        private HousekeeperService _houseKeeperService;
        private DateTime _statementDate = new DateTime(2017,1,1);
        private Housekeeper _houseKeeper;
        private string _statementFileName;
        #endregion

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper
            {
                Email = "housekeeperasdf",
                Oid = 1,
                FullName = "Housekeeper1",
                StatementEmailBody = "Statement body test"
            };

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(hr => hr.Query<Housekeeper>())
                .Returns(new List<Housekeeper> { _houseKeeper }.AsQueryable());

            _statementFileName = "fileName";
            _mockStatementGenerator = new Mock<IStatementGenerator>();
            _mockStatementGenerator
                .Setup(msg => msg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _mockEmailSender = new Mock<IEmailSender>();
            _mockXtraMessageBox = new Mock<IXtraMessageBox>();

            _houseKeeperService = new HousekeeperService(
                _mockUnitOfWork.Object, 
                _mockStatementGenerator.Object, 
                _mockEmailSender.Object, 
                _mockXtraMessageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _houseKeeperService.SendStatementEmails(_statementDate);

            _mockStatementGenerator.Verify(msg => msg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HouseKeepersEmailIsNullOrWhiteSpaceOrEmptyString_ShouldNotGenerateStatements(string newEmail)
        {
            _houseKeeper.Email = newEmail;

            _houseKeeperService.SendStatementEmails(_statementDate);

            _mockStatementGenerator.Verify(msg => msg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _houseKeeperService.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }     

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_StatementFileNameIsNullOrWhiteSpaceOrEmptyString_ShouldNotEmailStatements(string newStatement)
        {
            _statementFileName = newStatement;

            _houseKeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _mockEmailSender.Setup(mES => mES.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            ).Throws<Exception>();

            _houseKeeperService.SendStatementEmails(_statementDate);

            VerifyMessageBoxDisplay();
        }     

        #region[Private functions for class]
        private void VerifyEmailSent()
        {
             _mockEmailSender.Verify(mes => mes.EmailFile(
                _houseKeeper.Email,
                _houseKeeper.StatementEmailBody,
                _statementFileName, It.IsAny<string>())
            );
        }
        
        private void VerifyEmailNotSent()
        {
             _mockEmailSender.Verify(mes => mes.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(), 
                It.IsAny<string>()
            ), Times.Never);
        }

        private void VerifyMessageBoxDisplay()
        {
            _mockXtraMessageBox.Verify(mXMB => mXMB.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
        #endregion
    }
}
