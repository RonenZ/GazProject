using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gaz.DAL.Repositories;
using Gaz.DAL;
using System.Data;
using System.Data.Entity;


namespace GazDalTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCounterForUserID()
        {
            GazDBContext dbContext = new GazDBContext();
            var userRepository = new UserRepository(dbContext);
            var user = userRepository.GetUserDetails(1);


            Assert.AreEqual("moshemoshe", user.Username);
            
        }
    }
}
