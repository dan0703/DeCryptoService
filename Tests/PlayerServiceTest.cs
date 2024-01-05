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
            Implementations implementations = new Implementations();
            User validUser = new User
            {
                name = "Juan Carlos Pérez Arriaga",
                accountNickname = "elrevo",
                birthDay = "24/09/2003"
            };
            bool result = implementations.RegisterPlayer(validUser);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetSimilarNickNamesMatch()
        {
            List<String> expected = new List<string>
            {
                "user"
            };

            Implementations implementations = new Implementations();
            String nickname = "us";
            
            List<String> result = implementations.GetSimilarsNickNames(nickname);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetSimilarNickNamesNoMatch()
        {
            List<String> expected = new List<string>
            {
                "bonie"
            };

            Implementations implementations = new Implementations();
            String nickname = "boni";

            List<String> result = implementations.GetSimilarsNickNames(nickname);
            CollectionAssert.AreNotEqual(expected, result);
        }

        [TestMethod]
        public void ExistingNickname()
        {
            Implementations implementations = new Implementations();
            String nickname = "Sujey";
            bool result = implementations.ExistNickname(nickname);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NonexistentNickname()
        {
            Implementations implementations = new Implementations();
            String nickname = "mingi";
            bool result = implementations.ExistNickname(nickname);
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
