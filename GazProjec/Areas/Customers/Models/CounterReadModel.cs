using System;

namespace GazProjec.Areas.Customers.Models
{
    public class CounterReadModel
    {
      public int ReadID { get; set; }
      public int CounterID  { get; set; }
      public DateTime CreateTime { get; set; }
      public float ReadAmount { get; set; }
    }
}