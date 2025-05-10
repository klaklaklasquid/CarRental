using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Model {
    public class Establishment {
        private int _id;

        public int Id {
            get { return _id; }
            set {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Id));
                _id = value;
            }
        }

        private string _airport;

        public string Airport {
            get { return _airport; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Airport));
                _airport = value;
            }
        }

        private string _street;

        public string Street {
            get { return _street; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Street));
                _street = value;
            }
        }

        private int _zipcode;

        public int Zipcode {
            get { return _zipcode; }
            set {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Zipcode));
                _zipcode = value;
            }
        }

        private string _city;

        public string City {
            get { return _city; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(City));
                _city = value;
            }
        }

        private string _country;


        public string Country {
            get { return _country; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Country));
                _country = value;
            }
        }

        public Establishment(int id, string airport, string street, int zipcode, string city, string country) {
            Id = id;
            Airport = airport;
            Street = street;
            Zipcode = zipcode;
            City = city;
            Country = country;
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
