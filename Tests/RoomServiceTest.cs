using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Tests
{
    [TestClass]
    public class RoomServiceTest
    {
        private DeCryptoEntities context;
        private TransactionScope transaction;

        private Implementations implementations = new Implementations();

        [TestInitialize]
        public void TestInitialize()
        {
            context = new DeCryptoEntities();
            transaction = new TransactionScope();
        }

        [TestMethod]
        public void CreateRoom_ValidNickname_Successful()
        {
            string validNickname = "elrevo";
            int codeRoom = implementations.CreateRoom(validNickname);
            Assert.IsTrue(codeRoom > 0);
        }

        [TestMethod]
        public void CreateRoom_DuplicateCode_Retry()
        {
            int codeRoom1 = implementations.CreateRoom("elRevo");
            int codeRoom2 = implementations.CreateRoom("mingi");
            Assert.AreNotEqual(codeRoom1, codeRoom2);
        }

        [TestMethod]
        public void AllreadyExistRoom_ExistingRoom_True()
        {
            int existingCode = implementations.CreateRoom("sue");
            bool result = implementations.AllreadyExistRoom(existingCode);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AllreadyExistRoom_NonexistentRoom_False()
        {
            int nonExistentCode = 123456;
            bool result = implementations.AllreadyExistRoom(nonExistentCode);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsFullRoom_NotFullRoom_False()
        {
            int notFullCode = implementations.CreateRoom("sue");
            bool result = implementations.IsFullRoom(notFullCode);
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
