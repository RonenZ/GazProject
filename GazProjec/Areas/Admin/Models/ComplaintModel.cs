using System;

namespace GazProjec.Areas.Admin.Models
{
    public class ComplaintModel
    {
      public int ComplaintID { get; set; }
      public string ComplaintDescription  { get; set; }
      public int UserID { get; set; }
      public int CounterID { get; set; }
      public DateTime CreateTime { get; set; }
      public bool Disable { get; set; }
    }
}