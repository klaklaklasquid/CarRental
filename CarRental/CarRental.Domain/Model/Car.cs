using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Model {
    public class Car {
        private int _id;

        public int Id {
            get { return _id; }
            set {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Id));
                _id = value;
            }
        }

        private string _licencePlate;

        public string LicencePlate {
            get { return _licencePlate; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(LicencePlate));
                _licencePlate = value;
            }
        }

        private string _brand;

        public string Brand {
            get { return _brand; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Brand));
                _brand = value;
            }
        }

        private int _seats;

        public int Seats {
            get { return _seats; }
            set {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 2, nameof(Seats));
                _seats = value;
            }
        }

        private string _engineType;

        public string EngineType {
            get { return _engineType; }
            set {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(EngineType));
                if (value != "Diesel" &&
                    value != "Elektrisch" &&
                    value != "Benzine" &&
                    value != "Hybride")
                    throw new ArgumentException("Engine type must be either Diesel, Gasoline, Electric or Hybrid.", nameof(EngineType));
                _engineType = value;
            }
        }

        private int _airportId;

        public int AirportId {
            get { return _airportId; }
            set {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(AirportId));
                _airportId = value;
            }
        }


        public Car(int id, string licencePlate, string brand, int seats, string engineType, int airportId) {
            Id = id;
            LicencePlate = licencePlate;
            Brand = brand;
            Seats = seats;
            EngineType = engineType;
            AirportId = airportId;
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
