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
        private Implementations implementations;

        [TestInitialize]
        public void TestInitialize()
        {
            context = new DeCryptoEntities();
            transaction = new TransactionScope();
            implementations = new Implementations();
        }

        [TestMethod]
        public void RegisterPlayer_ValidPlayer_Successul()
        {
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
        public void RegisterPlayer_NullUser_ReturnsFalse()
        {
            User nullUser = null;
            bool result = implementations.RegisterPlayer(nullUser);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetSimilarNickNames_MatchNickname_Successful()
        {
            List<String> expected = new List<string>
            {
                "user"
            };
            String nickname = "us";
            
            List<String> result = implementations.GetSimilarsNickNames(nickname);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetSimilarNickNames_ExactMatch_Successful()
        {
            List<String> expected = new List<string>
            {
                "Sujey"
            };
            String nickname = "Sujey";

            List<String> result = implementations.GetSimilarsNickNames(nickname);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetSimilarNickNames_DifferentCasing_ReturnFalse()
        {
            List<String> expected = new List<string>
            {
                "su",
                "Sujey"
            };
            String nickname = "Su";

            List<String> result = implementations.GetSimilarsNickNames(nickname);
            CollectionAssert.AreNotEqual(expected, result);
        }

        [TestMethod]
        public void GetSimilarNickNames_NoMatchNicknames_Error()
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
        public void ExistNickname_AlreadyExist()
        {
            Implementations implementations = new Implementations();
            String nickname = "Sujey";
            bool result = implementations.ExistNickname(nickname);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExistNickname_NonexistentNickname()
        {
            Implementations implementations = new Implementations();
            String nickname = "mingi";
            bool result = implementations.ExistNickname(nickname);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExistNickname_NullNickname_ReturnsFalse()
        {
            Implementations implementations = new Implementations();
            String nickname = null;
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
