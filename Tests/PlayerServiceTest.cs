using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;
using Service;
using Domain.data;
using DataAccess;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class PlayerServiceTest
    {
        [TestMethod]
        public void Test01_RegisterPlayerSuccesfull()
        {
            IPlayerServices playerService = new Implementations();
            User validUser = new User
            {
                name = "Felix",
                accountNickname = "lixie",
                birthDay = "24/09/2003"
            };
            bool result = playerService.RegisterPlayer(validUser);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test02_GetSimilarNickNamesSuccessfull()
        {
            List<String> expected = new List<string>
            {
                "user"
            };

            IPlayerServices playerService = new Implementations();
            String nickname = "user";
            
            List<String> result = playerService.GetSimilarsNickNames(nickname);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test02_GetSimilarNickNamesFail()
        {
            List<String> expected = new List<string>
            {
                "bonie"
            };

            IPlayerServices playerService = new Implementations();
            String nickname = "bonie";

            List<String> result = playerService.GetSimilarsNickNames(nickname);
            CollectionAssert.AreNotEqual(expected, result);
        }
    }
}
