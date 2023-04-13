namespace WebAutopark.Models
{
    public class OrderItemViewModel
    {
        public int OrderId { get; set; }
        public string VehicleModelName { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string ComponentName { get; set; }
        public string Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
