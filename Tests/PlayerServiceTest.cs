using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;
using Service;
using Domain.data;
using DataAccess;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace Tests
{
    [TestClass]
    public class PlayerServiceTest
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
        public void RegisterPlayer()
        {
            IPlayerServices playerService = new Implementations();
            User validUser = new User
            {
                name = "Juan Carlos Pérez Arriaga",
                accountNickname = "elrevo",
                birthDay = "24/09/2003"
            };
            bool result = playerService.RegisterPlayer(validUser);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetSimilarNickNamesMatch()
        {
            List<String> expected = new List<string>
            {
                "user"
            };

            IPlayerServices playerService = new Implementations();
            String nickname = "us";
            
            List<String> result = playerService.GetSimilarsNickNames(nickname);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetSimilarNickNamesNoMatch()
        {
            List<String> expected = new List<string>
            {
                "bonie"
            };

            IPlayerServices playerService = new Implementations();
            String nickname = "boni";

            List<String> result = playerService.GetSimilarsNickNames(nickname);
            CollectionAssert.AreNotEqual(expected, result);
        }

        [TestMethod]
        public void ExistingNickname()
        {
            IPlayerServices playerService = new Implementations();
            String nickname = "Sujey";
            bool result = playerService.ExistNickname(nickname);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NonexistentNickname()
        {
            IPlayerServices playerService = new Implementations();
            String nickname = "mingi";
            bool result = playerService.ExistNickname(nickname);
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
