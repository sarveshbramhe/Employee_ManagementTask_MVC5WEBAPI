using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_ManagementMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public System.DateTime DOB { get; set; }
        public string Address { get; set; }
    }
}