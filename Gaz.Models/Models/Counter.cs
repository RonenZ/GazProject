using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class Counter : IBaseModel
    {
        public Counter()
        {
            this.CounterReads = new List<CounterRead>();
            this.User_Counter_Reference = new List<User_Counter>();
            this.UserBills = new List<UserBill>();
            this.UserComplaints = new List<UserComplaint>();
        }

        public int ID { get; set; }
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<CounterRead> CounterReads { get; set; }
        public virtual ICollection<User_Counter> User_Counter_Reference { get; set; }
        public virtual ICollection<UserBill> UserBills { get; set; }
        public virtual ICollection<UserComplaint> UserComplaints { get; set; }
    }
}
