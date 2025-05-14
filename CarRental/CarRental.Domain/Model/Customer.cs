using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarRental.Domain.Model {
    public class Customer {
        private int _id;

        public int Id {
            get { return _id; }
            set {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id cannot be negative");
                }
                _id = value;
            }
        }

        private string _firstName = string.Empty;

        public string FirstName {
            get { return _firstName; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("First name cannot be null, empty or whitespace", nameof(FirstName));
                }
                if (value.Any(char.IsDigit)) {
                    throw new ArgumentException("First name can't contain number(s)", nameof(FirstName));
                }
                _firstName = value;
            }
        }

        private string _lastName = string.Empty;

        public string LastName {
            get { return _lastName; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Last name cannot be null, empty or whitespace", nameof(LastName));
                }
                if (value.Any(char.IsDigit)) {
                    throw new ArgumentException("Last name can't contain number(s)", nameof(LastName));
                }
                _lastName = value;
            }
        }

        private string _email = string.Empty;
        private readonly string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public string Email {
            get { return _email; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Email cannot be null, empty or whitespace", nameof(Email));
                }
                if (!Regex.IsMatch(value, pattern)) {
                    throw new ArgumentException("Email is not valid", nameof(Email));
                }
                _email = value;
            }
        }

        private string _street = string.Empty;

        public string Street {
            get { return _street; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Street cannot be null, empty or whitespace", nameof(LastName));
                }
                if (!value.Any(char.IsDigit)) {
                    throw new ArgumentException("Street must contain at least one number", nameof(LastName));
                }
                _street = value;
            }
        }

        private string _zipcode = string.Empty;

        public string Zipcode {
            get { return _zipcode; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Zipcode cannot be null, empty or whitespace", nameof(LastName));
                }
                if (!value.Any(char.IsDigit)) {
                    throw new ArgumentException("Zipcode must contain at least one number", nameof(LastName));
                }
                _zipcode = value;
            }
        }

        private string _city = string.Empty;

        public string City {
            get { return _city; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("City cannot be null, empty or whitespace", nameof(LastName));
                }
                if (value.Any(char.IsDigit)) {
                    throw new ArgumentException("City can't contain number(s)", nameof(LastName));
                }
                _city = value;
            }
        }

        private string _country = string.Empty;

        public string Country {
            get { return _country; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Country cannot be null, empty or whitespace", nameof(LastName));
                }
                if (value.Any(char.IsDigit)) {
                    throw new ArgumentException("Country can't contain number(s)", nameof(LastName));
                }
                _country = value;
            }
        }

        public Customer(string firstName, string lastName, string email, string street, string zipcode, string city, string country) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Street = street;
            Zipcode = zipcode;
            City = city;
            Country = country;
        }

        public Customer(int id, string firstName, string lastName, string email, string street, string zipcode, string city, string country) : this(firstName, lastName, email, street, zipcode, city, country) {
            Id = id;
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
