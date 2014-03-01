using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class UserBill : IBaseModel
    {
        public int ID { get; set; }
        public float BillAmount { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int CounterID { get; set; }
        public bool Payed { get; set; }
        public virtual Counter Counter { get; set; }
    }
}
