using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Model {
    public class Customer {
        private int _id;

        public int Id {
            get { return _id; }
            set {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Id));
                _id = value;
            }
        }

        private string _firstName;

        public string FirstName {
            get { return _firstName; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(FirstName));
                _firstName = value;
            }
        }

        private string _lastName;

        public string LastName {
            get { return _lastName; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(LastName));
                _lastName = value;
            }
        }

        private string _email;

        public string Email {
            get { return _email; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(LastName));
                _email = value;
            }
        }

        private string _street;

        public string Street {
            get { return _street; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(LastName));
                _street = value;
            }
        }

        private int _zipcode;

        public int Zipcode {
            get { return _zipcode; }
            set {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(LastName));
                _zipcode = value;
            }
        }

        private string _city;

        public string City {
            get { return _city; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(LastName));
                _city = value;
            }
        }

        private string _country;

        public string Country {
            get { return _country; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(LastName));
                _country = value;
            }
        }

        public Customer(int id, string firstName, string lastName, string email, string street, int zipcode, string city, string country) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Street = street;
            Zipcode = zipcode;
            City = city;
            Country = country;
        }

        public override bool Equals(object? obj) {
            return obj is Customer customer &&
                   Id == customer.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }
    }
}
