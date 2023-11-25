using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;
using Service;
using Domain.data;
using DataAccess;

namespace Tests
{
    [TestClass]
    public class AccountServiceTest
    {
        [TestMethod]
        public void Test01_LoginSuccesfull()
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
        public void Test02_LoginFail()
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

        /*
        [TestMethod]
        public void Test03_RegisterAccountSuccesfull()
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
        */

        [TestMethod]
        public void Test04_VerifyEmailSuccesfull()
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
        public void Test05_VerifyEmailFail()
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
        public void Test06_ChangePasswordSucess()
        {
            IAccountServices accountService = new Implementations();
            Account validAccount = new Account
            {
                nickname = "lixie",
                email = "lixie01@gmail.com",
                emailVerify = false,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            String password = "e9d8ab9f2a9424dccb9e82e7a9b3e4e736f74323e0c8bbefb9b1d5b8cb24e1e0";
            bool result = accountService.ChangePassword(validAccount, password);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test07_ChangePasswordFail()
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
        public void Test08_IsCurrentPasswordSucessfull()
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
        public void Test09_IsCurrentPasswordFail()
        {
            IAccountServices accountService = new Implementations();
            Account invalidAccount = new Account
            {
                nickname = "lixie",
                email = "lixie01@gmail.com",
                emailVerify = false,
                password = "e9d8ab9f2a9424dccb9e82e7a9b3e4e736f74323e0c8bbefb9b1d5b8cb24e1e0"
            };
            String password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb";
            bool result = accountService.IsCurrentPassword(invalidAccount, password);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test10_ExistAccountSuccess()
        {
            IAccountServices accountService = new Implementations();
            String email = "lixie01@gmail.com";
            bool result = accountService.ExistAccount(email);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test11_ExistAccountFail()
        {
            IAccountServices accountService = new Implementations();
            String email = "lixie@gmail.com";
            bool result = accountService.ExistAccount(email);
            Assert.IsFalse(result);
        }
    }
}
