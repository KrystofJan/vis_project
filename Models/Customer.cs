using System;

namespace Models
{
    public class Customer
    {
        public String customer_id { get; set; }
        public PersonDetail person_detail { get; set; }
        public int total_rentals { get; set; }
    }
}
