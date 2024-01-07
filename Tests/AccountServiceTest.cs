using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;
using Service;
using Domain.data;
using DataAccess;
using System.Transactions;
using Microsoft.Win32;
using System.Text;

namespace Tests
{
    [TestClass]
    public class AccountServiceTest
    {
        private DeCryptoEntities context;
        private TransactionScope transaction;
        private Implementations implementations;

        [TestInitialize]
        public void TestInitialize()
        {
            context = new DeCryptoEntities();
            transaction = new TransactionScope();
            implementations = new Implementations();
        }

        [TestMethod]
        public void Login_LoginCorrectCredentials_Successful()
        {
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
        public void Login_NullAccount_ReturnsNull()
        {
            Account nullAccount = null;
            Account result = implementations.Login(nullAccount);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Login_LoginIncorrectEmail_Error()
        {
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
        public void Login_LoginIncorrectPassword_Error()
        {
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
        public void Login_NullPassword_ReturnsNull()
        {
            Account accountWithNullPassword = new Account
            {
                email = "existing@gmail.com",
                password = null
            };
            Account result = implementations.Login(accountWithNullPassword);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RegisterAccount_ExistenAccount_Error()
        {
            Account invalidAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey5420@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f44"
            };
            bool result = implementations.RegisterAccount(invalidAccount);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterAccount_NullAccount_ReturnsFalse()
        {
            Account nullAccount = null;
            bool result = implementations.RegisterAccount(nullAccount);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterAccount_NullEmail_ReturnsFalse()
        {
            Account accountWithNullEmail = new Account
            {
                email = null,
                password = "somepassword"
            };
            bool result = implementations.RegisterAccount(accountWithNullEmail);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterAccount_EmptyEmail_ReturnsFalse()
        {
            Account accountWithEmptyEmail = new Account
            {
                email = "",
                password = "somepassword"
            };
            bool result = implementations.RegisterAccount(accountWithEmptyEmail);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterAccount_ValidAccount_Succesfull()
        {
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
        public void VerifyEmail_NullAccount_ReturnsFalse()
        {
            Account nullAccount = null;
            bool result = implementations.VerifyEmail(nullAccount);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyEmail_EmptyEmail_ReturnsFalse()
        {
            Account accountWithEmptyEmail = new Account
            {
                email = "",
                emailVerify = false,
                password = "somepassword"
            };
            bool result = implementations.VerifyEmail(accountWithEmptyEmail);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyEmail_NullEmail_ReturnsFalse()
        {
            Account accountWithNullEmail = new Account
            {
                email = null,
                emailVerify = false,
                password = "somepassword"
            };
            bool result = implementations.VerifyEmail(accountWithNullEmail);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyEmail_ExistingEmail_Successful()
        {
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
        public void VerifyEmail_NotExistingEmail_Error()
        {
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
        public void ChangePassword_ExistingAccount_Successful()
        {
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
        public void ChangePassword_NullAccount_ReturnsFalse()
        {
            Account nullAccount = null;
            String newPassword = "e9d8ab9f2a9424dccb9e82e7a9b3e4e736f74323e0c8bbefb9b1d5b8cb24e1e0";
            bool result = implementations.ChangePassword(nullAccount, newPassword);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePassword_NullPassword_ReturnsFalse()
        {
            Account validAccount = new Account
            {
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String nullPassword = null;
            bool result = implementations.ChangePassword(validAccount, nullPassword);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePassword_EmptyPassword_ReturnsFalse()
        {
            Account validAccount = new Account
            {
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String emptyPassword = "";
            bool result = implementations.ChangePassword(validAccount, emptyPassword);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePassword_NonexistentAccount_Successful()
        {
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
        public void IsCurrentPassword_MatchPasswords_Successful()
        {
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
        public void IsCurrentPassword_NullAccount_ReturnsFalse()
        {
            Account nullAccount = null;
            String password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb";
            bool result = implementations.IsCurrentPassword(nullAccount, password);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsCurrentPassword_NullPassword_ReturnsFalse()
        {
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String nullPassword = null;
            bool result = implementations.IsCurrentPassword(validAccount, nullPassword);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsCurrentPassword_EmptyPassword_ReturnsFalse()
        {
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String emptyPassword = "";
            bool result = implementations.IsCurrentPassword(validAccount, emptyPassword);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsCurrentPassword_NoMatchPasswords_Error()
        {
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
        public void ExistAccount_ValidEmail_Successful()
        {
            String email = "user@gmail.com";
            bool result = implementations.ExistAccount(email);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExistAccount_NonexistentEmail_Error()
        {
            String email = "lixie@gmail.com";
            bool result = implementations.ExistAccount(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExistAccount_NullEmail_Error()
        {
            String email = null;
            bool result = implementations.ExistAccount(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEmailVerified_ValidAccountAndVerified_True()
        {
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            bool result = implementations.IsEmailVerified(validAccount);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEmailVerified_ValidAccountAndNotVerified_False()
        {
            Account validAccount = new Account
            {
                nickname = "other",
                email = "mingilix8@gmail.com",
                emailVerify = false,
                password = "20a69db924fbc16bbf478373baf9e1abd1dc8e0b338b8a8cd4033ec3defacea0"
            };

            bool result = implementations.IsEmailVerified(validAccount);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEmailVerified_NullAccount_False()
        {
            Account nullAccount = null;
            bool result = implementations.IsEmailVerified(nullAccount);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEmailVerified_NonexistentAccount_False()
        {
            Account nonExistentAccount = new Account
            {
                email = "nonexistent@gmail.com",
                emailVerify = true
            };
            bool result = implementations.IsEmailVerified(nonExistentAccount);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEmailRegistered_ExistingEmail_True()
        {
            string existingEmail = "user@gmail.com";
            bool result = implementations.IsEmailRegistered(existingEmail);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEmailRegistered_NonexistentEmail_False()
        {
            string nonExistentEmail = "nonexistent@gmail.com";
            bool result = implementations.IsEmailRegistered(nonExistentEmail);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsEmailRegistered_NullEmail_False()
        {
            string nullEmail = null;
            bool result = implementations.IsEmailRegistered(nullEmail);
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