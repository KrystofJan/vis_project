using System;

namespace Models
{
    public class PersonDetail
    {
        public int person_detail_id { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public Address address { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_data { get; set; }

    }
}