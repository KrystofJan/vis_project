using System;

namespace Models
{
    public class Customer : PersonDetail
    {
        public String customer_id { get; set; }
        public int total_rentals { get; set; }
    }
}
