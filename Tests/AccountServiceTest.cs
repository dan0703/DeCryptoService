using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;
using Service;
using Domain.data;
using DataAccess;
using System.Transactions;

namespace Tests
{
    [TestClass]
    public class AccountServiceTest
    {
        private DeCryptoEntities context;
        private TransactionScope transaction;

        
        [TestInitialize]
        public void TestInitialize()
        {
            context = new DeCryptoEntities();
            transaction = new TransactionScope();
        }

        [TestMethod]
        public void LoginCorrectCredentials()
        {   
            IAccountServices accountService = new Implementations();
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            Account result = accountService.Login(validAccount);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoginIncorrectEmail()
        {
            IAccountServices accountService = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey5420@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            Account result = accountService.Login(invalidAccount);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoginIncorrectPassword()
        {
            IAccountServices accountService = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey5420@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f44"
            };
            Account result = accountService.Login(invalidAccount);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RegisterAccountSuccesfull()
        {
            IAccountServices accountService = new Implementations();
            Account validAccount = new Account
            {
                nickname = "lixie",
                email = "lixie01@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            bool result = accountService.RegisterAccount(validAccount);
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void VerifyExistingEmail()
        {
            IAccountServices accountService = new Implementations();
            Account validAccount = new Account
            {
                nickname = "user",
                email = "user@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            bool result = accountService.VerifyEmail(validAccount);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyNotExistingEmail()
        {
            IAccountServices accountService = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "user",
                email = "use@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            bool result = accountService.VerifyEmail(invalidAccount);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePasswordExistingAccount()
        {
            IAccountServices accountService = new Implementations();
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String password = "e9d8ab9f2a9424dccb9e82e7a9b3e4e736f74323e0c8bbefb9b1d5b8cb24e1e0";
            bool result = accountService.ChangePassword(validAccount, password);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ChangePasswordNonexistentAccount()
        {
            IAccountServices accountService = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "lixie",
                email = "lixie@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String password = "e9d8ab9f2a9424dccb9e82e7a9b3e4e736f74323e0c8bbefb9b1d5b8cb24e1e0";
            bool result = accountService.ChangePassword(invalidAccount, password);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsCurrentPasswordMatch()
        {
            IAccountServices accountService = new Implementations();
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb";
            bool result = accountService.IsCurrentPassword(validAccount, password);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsCurrentPasswordNoMatch()
        {
            IAccountServices accountService = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "other",
                email = "mingilix8@gmail.com",
                emailVerify = false,
                password = "20a69db924fbc16bbf478373baf9e1abd1dc8e0b338b8a8cd4033ec3defacea0"
            };
            String password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb";
            bool result = accountService.IsCurrentPassword(invalidAccount, password);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExistAccount()
        {
            IAccountServices accountService = new Implementations();
            String email = "user@gmail.com";
            bool result = accountService.ExistAccount(email);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NonexistentAccount()
        {
            IAccountServices accountService = new Implementations();
            String email = "lixie@gmail.com";
            bool result = accountService.ExistAccount(email);
            Assert.IsFalse(result);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Dispose();
            context.Dispose();
        }
    }
}
