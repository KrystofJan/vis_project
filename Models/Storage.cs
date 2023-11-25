using System;
using System.Collections.ObjectModel;

namespace Models
{
    public class Storage
    {
        public int storage_id { get; set; }
        public String storage_name { get; set; }
        public Address address { get; set; }
        public Collection<Stock> stocks { get; set; }
    }
}