using System;
using System.Collections.Generic;
using System.Text;

namespace Lhs.Entity.ForeignDtos.Response.Task
{
    public class TaskDetailsResponse
    {
        public string ApplyNum { get; set; } // KL41256156412
        public string Status { get; set; }//"1"
        public string PlanDate { get; set; } //"2019.09.10-2019.09.12",
        public string Urgent { get; set; } //false
        public string AskDate { get; set; } //"2019.09.10-2019.09.15",
        public string Personnel { get; set; }// "陈"
        public string Contact { get; set; }// "12345678910"
    }
}
