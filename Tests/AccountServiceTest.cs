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
            Implementations implementations = new Implementations();
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            Account result = implementations.Login(validAccount);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoginIncorrectEmail()
        {
            Implementations implementations = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey5420@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            Account result = implementations.Login(invalidAccount);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoginIncorrectPassword()
        {
            Implementations implementations = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey5420@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f44"
            };
            Account result = implementations.Login(invalidAccount);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RegisterAccountSuccesfull()
        {
            Implementations implementations = new Implementations();
            Account validAccount = new Account
            {
                nickname = "lixie",
                email = "lixie01@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            bool result = implementations.RegisterAccount(validAccount);
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void VerifyExistingEmail()
        {
            Implementations implementations = new Implementations();
            Account validAccount = new Account
            {
                nickname = "user",
                email = "user@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            bool result = implementations.VerifyEmail(validAccount);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyNotExistingEmail()
        {
            Implementations implementations = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "user",
                email = "use@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            bool result = implementations.VerifyEmail(invalidAccount);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePasswordExistingAccount()
        {
            Implementations implementations = new Implementations();
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String password = "e9d8ab9f2a9424dccb9e82e7a9b3e4e736f74323e0c8bbefb9b1d5b8cb24e1e0";
            bool result = implementations.ChangePassword(validAccount, password);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ChangePasswordNonexistentAccount()
        {
            Implementations implementations = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "lixie",
                email = "lixie@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String password = "e9d8ab9f2a9424dccb9e82e7a9b3e4e736f74323e0c8bbefb9b1d5b8cb24e1e0";
            bool result = implementations.ChangePassword(invalidAccount, password);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsCurrentPasswordMatch()
        {
            Implementations implementations = new Implementations();
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb";
            bool result = implementations.IsCurrentPassword(validAccount, password);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsCurrentPasswordNoMatch()
        {
            Implementations implementations = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "other",
                email = "mingilix8@gmail.com",
                emailVerify = false,
                password = "20a69db924fbc16bbf478373baf9e1abd1dc8e0b338b8a8cd4033ec3defacea0"
            };
            String password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb";
            bool result = implementations.IsCurrentPassword(invalidAccount, password);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExistAccount()
        {
            Implementations implementations = new Implementations();
            String email = "user@gmail.com";
            bool result = implementations.ExistAccount(email);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NonexistentAccount()
        {
            Implementations implementations = new Implementations();
            String email = "lixie@gmail.com";
            bool result = implementations.ExistAccount(email);
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
