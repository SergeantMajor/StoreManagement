using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Model
{
    public class Store
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(2, ErrorMessage = "Country Code could not be more than two symbols")]
        public string CountryCode { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string StoreManagerName { get; set; }
        [Required]
        public string StoreManagerLastName { get; set; }
        [Required]
        [EmailAddress]
        public string StoreManagerEmail { get; set; }
        [Required]
        public int Category { get; set; }
    }

    public class StoreCharacters: Store
    {
        public int StockBackStore { get; set; }
        public int StockFrontStore { get; set; }
        public int StockShoppingWindow { get; set; }
        public double StockAccuracy { get; set; }
        public double OnFloorAvailability { get; set; }
        public int StockMeanAgeDays { get; set; }
        public int TotalStock { get; set; }
    }
}
