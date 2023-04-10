namespace WebAutopark.Models
{
    public class VehicleGetByIdViewModel
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }
        public string Model { get; set; }
        public string? RegistrationNumber { get; set; }
        public string Weight { get; set; }
        public string Year { get; set; }
        public string Mileage { get; set; }
        public string Color { get; set; }
        public string FuelConsumption { get; set; }
        public string TankCapacity { get; set; }
        public string TaxPerMonth { get; set; }
        public string MaxKilometersOnTank { get; set; }
    }
}
