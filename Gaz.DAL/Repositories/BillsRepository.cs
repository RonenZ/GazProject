﻿using Gaz.Models.Models;
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
    public class BillsRepository : GenericRepository<UserBill>
    {
        public BillsRepository(DbContext context) : base(context) { }

        /// <summary>
        /// returns last bill for counter, using sp
        /// </summary>
        public UserBill GetLastBillByCounterID(int counterID)
        {
            return this.usp_GetLastBillForCounter(counterID).SingleOrDefault();
        }

        /// <summary>
        /// returns all open bills of counter, using sp
        /// </summary>
        public IEnumerable<UserBill> GetOpenBillsByCounterID(int counterID)
        {
            return this.usp_GetOpenBillsForCounter(counterID);
        }


        /// <summary>
        /// returns bill by counter and date
        /// </summary>
        public UserBill GetBillByDate(int counterId, DateTime date)
        {
            var nextMonth = date.AddMonths(1);
            return DbSet.FirstOrDefault(f => f.CreateTime >= date && f.CreateTime <= nextMonth && f.CounterID == counterId);
        }

        /// <summary>
        /// returns all bills for counter
        /// </summary>
        public IEnumerable<UserBill> GetAllBillsByCounterId(int counterID)
        {
            return this.usp_GetBillForCounter(counterID);
        }

        #region Stored Procedures

        protected virtual IEnumerable<UserBill> usp_GetBillForCounter(int? counterId)
        {
            var counterIdp = new SqlParameter("@counterID", counterId);

            return DbContext.Database
                            .SqlQuery<UserBill>("usp_GetBillForCounter @counterID", counterIdp);
        }


        protected virtual IEnumerable<UserBill> usp_GetLastBillForCounter(int? counterID)
        {
            var counterIDP = new SqlParameter("@counterID", counterID);

            return DbContext.Database
                            .SqlQuery<UserBill>("usp_GetLastBillForCounter @counterID", counterIDP);
        }

        protected virtual IEnumerable<UserBill> usp_GetOpenBillsForCounter(int? counterID)
        {
            var counterIdp = new SqlParameter("@counterID", counterID);

            return DbContext.Database
                            .SqlQuery<UserBill>("usp_GetOpenBillsForCounter @counterID", counterIdp);
        }

        #endregion

    }
}
