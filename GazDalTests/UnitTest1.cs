using System;
using Gaz.DAL.DbContexts;
using GazProjec.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gaz.DAL.Repositories;


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

        [TestMethod]
        public void SendComplaintReadMail()
        {
            bool isSent = false;

            using (var db = new GazDbContext())
            {
                var repo = new UserComplaintsRepository(db);

                var result = repo.GetByID(1005);

                if (result != null)
                {
                    result.Disable = true;

                    try
                    {
                        repo.Commit();

                        using (var ms = new MailingService())
                        {
                            isSent = ms.SendMail_UserComplaintRead(result);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            Assert.IsTrue(isSent);
        }
    }
}
