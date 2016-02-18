using System;
using SoftTelekom.Core.Models.Bases;

namespace SoftTelekom.Core.Models
{
    public class InternetUsageItem : BaseModel
    {
        public DateTime Date { get; set; }
        public string DataUsage { get; set; }
    }
}