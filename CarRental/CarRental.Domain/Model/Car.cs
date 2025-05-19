using CarRental.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarRental.Domain.Model {
    public class Car {
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

        private string _licencePlate = string.Empty;
        private readonly string pattern = @"^[A-Za-z]{2}-[A-Za-z]{3}-\d{3}$";

        public string LicencePlate {
            get { return _licencePlate; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Licence plate cannot be null, empty or whitespace.", nameof(LicencePlate));
                }
                if (!Regex.IsMatch(value, pattern)) {
                    throw new ArgumentException("Licence plate must be in the format XX-XXX-000.", nameof(LicencePlate));
                }
                _licencePlate = value;
            }
        }

        private string _brand = string.Empty;

        public string Brand {
            get { return _brand; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Brand cannot be null, empty or whitespace.", nameof(Brand));
                }
                _brand = value;
            }
        }

        private int _seats;

        public int Seats {
            get { return _seats; }
            set {
                if (value < 2 || value > 10) {
                    throw new ArgumentOutOfRangeException(nameof(Seats), "Seats must be between 2 and 10.");
                }

                _seats = value;
            }
        }

        private string _engineType = string.Empty;
        private readonly string[] allowedEngineTypes = new[] { "Diesel", "Electric", "Gasoline", "Hybrid" };

        public string EngineType {
            get { return _engineType; }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Engine type cannot be null, empty or whitespace.", nameof(EngineType));
                }
                if (!allowedEngineTypes.Contains(value)) {
                    throw new ArgumentException("Engine type must be either Diesel, Gasoline, Electric or Hybrid.", nameof(EngineType));
                }
                _engineType = value;
            }
        }

        private int _airportId;

        public int AirportId {
            get { return _airportId; }
            set {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException(nameof(AirportId), "Airport ID cannot be negative.");
                }
                _airportId = value;
            }
        }


        public Car(string licencePlate, string brand, int seats, string engineType, int airportId) {
            LicencePlate = licencePlate;
            Brand = brand;
            Seats = seats;
            EngineType = engineType;
            AirportId = airportId;
        }

        public Car(CarDTO car) {
            Id = car.Id;
            LicencePlate = car.LicensePlate;
            Brand = car.Brand;
            Seats = car.Seats;
            EngineType = car.EngineType;
            AirportId = car.AirportId;
        }

        public Car(int id, string licencePlate, string brand, int seats, string engineType, int airportId) : this(licencePlate, brand, seats, engineType, airportId) {
            Id = id;
        }

        public override bool Equals(object? obj) {
            return obj is Car car &&
                   Id == car.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }
    }
}
