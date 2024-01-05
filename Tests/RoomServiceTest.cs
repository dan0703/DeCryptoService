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
        private int codeRoom;

        [TestInitialize]
        public void TestInitialize()
        {
            context = new DeCryptoEntities();
            codeRoom = implementations.CreateRoom("mingilix");
            transaction = new TransactionScope();
        }

        [TestMethod]
        public void CreateRoomSuccessful()
        {
            int codeRoom = implementations.CreateRoom("elrevo");
        }

        [TestMethod]
        public void NotFullRoom()
        {
            var result = implementations.IsFullRoom(codeRoom);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExistentRoom()
        {
            var result = implementations.AllreadyExistRoom(codeRoom);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NonexistentRoom()
        {
            int invalidCode = 123456;
            var result = implementations.AllreadyExistRoom(invalidCode); 
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
