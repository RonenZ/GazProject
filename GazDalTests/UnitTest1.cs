using System;
using Gaz.DAL.DbContexts;
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
            GazDbContext dbContext = new GazDbContext();
            var userRepository = new UserRepository(dbContext);
            var user = userRepository.GetUserDetails(1);


            Assert.AreEqual("moshemoshe", user.Username);
            
        }
    }
}
