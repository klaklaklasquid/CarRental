using CarRental.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Model {
    public class Establishment {
        private int _id;

        public int Id {
            get { return _id; }
            set {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id cannot be negative.");
                }
                _id = value;
            }
        }

        private string _airport = string.Empty;

        public string Airport {
            get { return _airport; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Airport cannot be null, empty or whitespace.", nameof(Airport));
                }
                if (value.Any(char.IsDigit)) {
                    throw new ArgumentException("Airport name can't contain number(s).", nameof(Airport));
                }
                _airport = value;
            }
        }

        private string _street = string.Empty;

        public string Street {
            get { return _street; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Street cannot be null, empty or whitespace.", nameof(Street));
                }
                if (!value.Any(char.IsDigit)) {
                    throw new ArgumentException("Street must contain at least one number.", nameof(Street));
                }
                _street = value;
            }
        }

        private string _zipcode = string.Empty;

        public string Zipcode {
            get { return _zipcode; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Zipcode cannot be null, empty or whitespace.", nameof(Zipcode));
                }
                if (!value.Any(char.IsDigit)) {
                    throw new ArgumentException("Zipcode must contain at least one number.", nameof(Zipcode));
                }
                _zipcode = value;
            }
        }

        private string _city = string.Empty;

        public string City {
            get { return _city; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("City cannot be null, empty or whitespace.", nameof(City));
                }
                if (value.Any(char.IsDigit)) {
                    throw new ArgumentException("City name can't contain number(s).", nameof(City));
                }
                _city = value;
            }
        }

        private string _country = string.Empty;

        public string Country {
            get { return _country; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Country cannot be null, empty or whitespace.", nameof(Country));
                }
                if (value.Any(char.IsDigit)) {
                    throw new ArgumentException("Country name can't contain number(s).", nameof(Country));
                }
                _country = value;
            }
        }

        public Establishment(string airport, string street, string zipcode, string city, string country) {
            Airport = airport;
            Street = street;
            Zipcode = zipcode;
            City = city;
            Country = country;
        }

        public Establishment(EstablishmentDTO establishment) {
            Id = establishment.Id;
            Airport = establishment.Airport;
            Street = establishment.Street;
            Zipcode = establishment.Zipcode;
            City = establishment.City;
            Country = establishment.Country;
        }

        public Establishment(int id, string airport, string street, string zipcode, string city, string country)
            : this(airport, street, zipcode, city, country) {
            Id = id;
        }

        public override bool Equals(object? obj) {
            return obj is Establishment establishment &&
                   Id == establishment.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }
    }
}
