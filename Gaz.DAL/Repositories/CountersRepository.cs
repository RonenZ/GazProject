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
        public IEnumerable<Counter> GetCountersByUserId(int userID)
        {
            return this.usp_GetCountersForUser(userID);
        }

        /// <summary>
        /// returns counter reads of a counter per period, using sp
        /// </summary>
        public IEnumerable<CounterRead> GetCounterReadsPerPeriod(int counterID, DateTime startTime, DateTime endTime)
        {
            return this.usp_GetCounterReadPerPeriod(counterID, startTime, endTime);
        }

        #region Stored Procedures

        protected virtual IEnumerable<Counter> usp_GetCountersForUser(Nullable<int> usrID)
        {
            var usrIDP = new SqlParameter("@usrID", usrID);

            return DbContext.Database
                            .SqlQuery<Counter>("usp_GetCountersForUser @usrID", usrIDP);
        }


        public virtual IEnumerable<CounterRead> usp_GetCounterReadPerPeriod(Nullable<int> counterID, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime)
        {
            var counterIDP = new SqlParameter("@CounterID", counterID);
            var startTimeP = new SqlParameter("@StartTime", startTime);
            var endTimeP = new SqlParameter("@EndTime", endTime);

            return DbContext.Database
                            .SqlQuery<CounterRead>("usp_GetCounterReadPerPeriod @CounterID, @StartTime, @EndTime", counterIDP, startTime, endTime);
        }

        #endregion
    }
}
