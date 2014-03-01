using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class User_Counter
    {
        public int CounterID { get; set; }
        public int UserID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public virtual Counter Counter { get; set; }
        public virtual User User { get; set; }
    }
}
