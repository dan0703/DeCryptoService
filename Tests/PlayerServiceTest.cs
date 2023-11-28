using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;
using Service;
using Domain.data;
using DataAccess;

namespace Tests
{
    [TestClass]
    public class PlayerServiceTest
    {
        [TestMethod]
        public void Test01_RegisterPlayerSuccesfull()
        {
            IPlayerServices playerServices = new Implementations();
            User validUser = new User();
            {
                
            };
            bool result = playerServices.RegisterPlayer(validUser);
            Assert.IsTrue(result);
        }
    }
}
