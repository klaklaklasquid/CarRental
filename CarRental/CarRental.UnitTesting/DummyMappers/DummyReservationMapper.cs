using CarRental.Domain.DTOs;
using CarRental.Domain.Model;
using CarRental.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.UnitTesting.DummyMappers {
    class DummyReservationMapper : IReservationRepository {
        public List<ReservationDTO> GetReservations() {
            var customer1 = new CustomerDTO(
                "John", "Doe", "john.doe@example.com", "123 Main St", "12345", "Sample City", "Sample Country"
            );
            var car1 = new CarDTO(
                1, "AB-XYZ-123", "Toyota", 5, "Hybrid", 1
            );
            var establishment1 = new EstablishmentDTO(
                1, "Sample Airport", "456 Airport Rd", "54321", "Airport City", "Sample Country"
            );
            var reservation1 = new ReservationDTO(
                customer1,
                new DateTime(2024, 1, 1, 10, 0, 0),
                new DateTime(2024, 1, 5, 10, 0, 0),
                car1,
                establishment1
            );

            var customer2 = new CustomerDTO(
                "Jane", "Smith", "jane.smith@example.com", "789 Elm St", "67890", "Another City", "Sample Country"
            );
            var car2 = new CarDTO(
                2, "CD-ABC-456", "Honda", 4, "Electric", 2
            );
            var establishment2 = new EstablishmentDTO(
                2, "Downtown Branch", "101 Center St", "98765", "Downtown", "Sample Country"
            );
            var reservation2 = new ReservationDTO(
                customer2,
                new DateTime(2024, 2, 10, 9, 0, 0),
                new DateTime(2024, 2, 15, 9, 0, 0),
                car2,
                establishment2
            );

            var customer3 = new CustomerDTO(
                "Alice", "Brown", "alice.brown@example.com", "321 Oak Ave", "24680", "Uptown", "Sample Country"
            );
            var car3 = new CarDTO(
                3, "EF-GHI-789", "Ford", 7, "Diesel", 3
            );
            var establishment3 = new EstablishmentDTO(
                3, "Uptown Office", "202 Uptown Blvd", "13579", "Uptown", "Sample Country"
            );
            var reservation3 = new ReservationDTO(
                customer3,
                new DateTime(2024, 3, 20, 8, 0, 0),
                new DateTime(2024, 3, 25, 8, 0, 0),
                car3,
                establishment3
            );

            return new List<ReservationDTO> { reservation1, reservation2, reservation3 };
        }

        public void DeleteReservation(Reservation reservation) {
            throw new NotImplementedException();
        }

        public void SetReservation(Reservation reservation) {
            throw new NotImplementedException();
        }

        public void WipeDatabase() {
            throw new NotImplementedException();
        }
    }
}
