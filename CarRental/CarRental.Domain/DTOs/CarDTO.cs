using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.DTOs {
    public class CarDTO {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public int Seats { get; set; }
        public string EngineType { get; set; }
        public int AirportId { get; set; }

        public CarDTO(int id, string licensePlate, string brand, int seats, string engineType, int airportId) {
            Id = id;
            LicensePlate = licensePlate;
            Brand = brand;
            Seats = seats;
            EngineType = engineType;
            AirportId = airportId;
        }

        public override string? ToString() {
            return $"Model: {Brand}\nLicense plate: {LicensePlate}\nSeats: {Seats}\nEngine type: {EngineType}";
        }
    }
}
