using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gaz.Models.Models.Mapping;
using Gaz.Models.Models;
using Gaz.DAL.Repositories;


namespace GazDalTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCounterForUserID()
        {
            var dbContext = new GazDBContext();
            var userRepository = new UserRepository(dbContext);
            var user = userRepository.GetByID(1);


            Assert.AreEqual("moshemoshe", user.Username);
            
        }
    }
}
