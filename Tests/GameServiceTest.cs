using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Tests
{
    [TestClass]
    public class GameServiceTest
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
        public void GetBlueTeamWords_ValidCode_ReturnsValidList()
        {
            int validCode = implementations.CreateRoom("hostUser");
            List<string> result = implementations.GetBlueTeamWords(validCode);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetBlueTeamWords_InvalidCode_ReturnsEmptyList()
        {
            int invalidCode = 123456;
            List<string> result = implementations.GetBlueTeamWords(invalidCode);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 0);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Dispose();
            context.Dispose();
        }
    }
}
