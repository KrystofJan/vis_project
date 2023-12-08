using System;
using System.Collections.ObjectModel;

namespace Models;

public class RentalPost
{
    public Collection<Movie> movies { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string customer_id { get; set; }
    public string employee_id { get; set; }
    public int storage_id { get; set; }
}