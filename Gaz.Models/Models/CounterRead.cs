using System;
using System.Collections.Generic;

namespace Gaz.Models.Models
{
    public partial class CounterRead : IBaseModel
    {
        public int ID { get; set; }
        public int CounterID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public float ReadAmount { get; set; }
        public virtual Counter Counter { get; set; }
    }
}
