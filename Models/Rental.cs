using System;
using System.Collections.ObjectModel;

namespace Models
{
    public class Rental
    {
        public int rental_id { get; set; }
        public DateTime date_of_start { get; set; }
        public DateTime date_of_return { get; set; }
        public Customer customer { get; set; }
        public Employee employee { get; set; }
        public Collection<Disc> discs { get; set; }
        public bool is_returned { get; set; }
    }
}