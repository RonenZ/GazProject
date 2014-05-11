using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GazProjec.Models
{
    public class UserBillModel
    {
        public UserBillModel()
        {
        }

        public UserBillModel(string name, string amount, DateTime date, string billId, string counter)
        {
            Name = name;
            Amount = amount;
            Date = date;
            BillId = billId;
            Counter = counter;
        }

        public string Name { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public string Counter { get; set; }
        public string BillId { get; set; }
    }
}