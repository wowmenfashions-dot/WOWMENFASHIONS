using System;

namespace wowmenfashions.Models
{
    public class CustomerAddressDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsDefaultShipping { get; set; }
        public bool IsDefaultBilling { get; set; }
    }
}
