using System;
using SoftTelekom.Core.Models.Bases;

namespace SoftTelekom.Core.Models
{
    public class BillItem : BaseModel
    {
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public string BillURL { get; set; }
    }
}