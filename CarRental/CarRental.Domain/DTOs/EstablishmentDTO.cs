using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.DTOs {
    public class EstablishmentDTO {
        public string Airport { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public EstablishmentDTO(string airport, string street, string zipcode, string city, string country) {
            Airport = airport;
            Street = street;
            Zipcode = zipcode;
            City = city;
            Country = country;
        }
    }
}
