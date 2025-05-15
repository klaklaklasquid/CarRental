using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.DTOs {
    public class CustomerDTO {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public CustomerDTO(string firstName, string lastName, string email, string street, string zipcode, string city, string country) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Street = street;
            Zipcode = zipcode;
            City = city;
            Country = country;
        }

        public override string? ToString() {
            return $"{FirstName} {LastName}\n{Email}";
        }
    }
}
