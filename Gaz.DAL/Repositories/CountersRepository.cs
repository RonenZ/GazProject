using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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

        protected virtual ObjectResult<Counter> usp_GetCountersForUser(Nullable<int> usrID)
        {
            var usrIDParameter = usrID.HasValue ?
                new ObjectParameter("usrID", usrID) :
                new ObjectParameter("usrID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Counter>("usp_GetCountersForUser", usrIDParameter);
        }


        public virtual ObjectResult<CounterRead> usp_GetCounterReadPerPeriod(Nullable<int> counterID, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime)
        {
            var counterIDParameter = counterID.HasValue ?
                new ObjectParameter("CounterID", counterID) :
                new ObjectParameter("CounterID", typeof(int));

            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));

            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CounterRead>("usp_GetCounterReadPerPeriod", counterIDParameter, startTimeParameter, endTimeParameter);
        }

        #endregion
    }
}
