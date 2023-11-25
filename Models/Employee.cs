using System;

namespace Models
{
    public class Employee : PersonDetail
    {
        public String employee_id { get; set; }
        // public PersonDetail person_detail { get; set; }
        public decimal salary { get; set; }
        public Storage storage { get; set; }
    }
}