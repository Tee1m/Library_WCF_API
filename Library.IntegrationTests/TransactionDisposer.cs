using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Transactions;

namespace Library.IntegrationTests
{
    [TestClass]
    public class TransactionIsolator
    {
        protected TransactionScope scope;

        [TestInitialize]
        public void Initialize()
        {
            this.scope = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.scope.Dispose();
        }
    }

}

