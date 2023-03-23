using CarService.Domain.Models;

namespace CarService.Data
{
    public class CreatePurchaseOrderCredentials
    {
        public Client Client { get; set; }
        public Car Car { get; set; }
        public User Diagnostician { get; set; }
        public User Mechanic { get; set; }
    }
}
