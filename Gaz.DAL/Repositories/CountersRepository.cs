using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Gaz.DAL.Repositories
{
    public class CountersRepository : GenericRepository<Counter>
    {
        public CountersRepository(DbContext context) : base(context) { }

        /// <summary>
        /// returns all of user counters info by userid, using sp
        /// </summary>
        public IEnumerable<Counter> GetCountersByUserId(int userId)
        {
            return this.usp_GetCountersForUser(userId);
        }

        /// <summary>
        /// returns counter reads of a counter per period, using sp
        /// </summary>
        public IEnumerable<CounterRead> GetCounterReadsPerPeriod(int counterId, DateTime startTime, DateTime endTime)
        {
            return DbContext.Set<CounterRead>()
                .Where(w => w.CounterID == counterId && w.CreateTime >= startTime && w.CreateTime <= endTime);
            //return this.usp_GetCounterReadPerPeriod(counterId, startTime, endTime);
        }

        #region Stored Procedures

        protected virtual IEnumerable<Counter> usp_GetCountersForUser(int? usrId)
        {
            var usrIdp = new SqlParameter("@usrID", usrId);

            return DbContext.Database
                            .SqlQuery<Counter>("usp_GetCountersForUser @usrID", usrIdp);
        }


        public virtual IEnumerable<CounterRead> usp_GetCounterReadPerPeriod(int? counterId, DateTime? startTime, DateTime? endTime)
        {
            var counterIdp = new SqlParameter("@CounterID", counterId);
            var startTimeP = new SqlParameter("@StartTime", startTime);
            var endTimeP = new SqlParameter("@EndTime", endTime);

            return DbContext.Database
                            .SqlQuery<CounterRead>("usp_GetCounterReadPerPeriod @CounterID, @StartTime, @EndTime", counterIdp, startTimeP, endTimeP)
                            .ToList();
        }

        #endregion
    }
}
