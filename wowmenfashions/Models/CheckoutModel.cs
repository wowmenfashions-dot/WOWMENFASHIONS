using System.ComponentModel.DataAnnotations;

namespace wowmenfashions.Models
{
    public class CheckoutModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address Line 1 is required")]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string AddressLine1 { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Address Line 2 cannot exceed 500 characters")]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal Code is required")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = "India";

        [Required(ErrorMessage = "Contact Number is required")]
        [StringLength(50, ErrorMessage = "Contact Number cannot exceed 50 characters")]
        public string ContactNumber { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Landmark cannot exceed 255 characters")]
        public string? Landmark { get; set; }
    }
}
