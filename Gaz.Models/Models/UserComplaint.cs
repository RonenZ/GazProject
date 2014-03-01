using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class UserComplaint : IBaseModel
    {
        public int ID { get; set; }
        public string ComplaintDescription { get; set; }
        public int UserID { get; set; }
        public int CounterID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public bool Disable { get; set; }
        public virtual Counter Counter { get; set; }
        public virtual User User { get; set; }
    }
}
