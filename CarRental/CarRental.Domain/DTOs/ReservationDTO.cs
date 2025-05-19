using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.DTOs {
    public class ReservationDTO {
        public CustomerDTO Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarDTO Car { get; set; }
        public EstablishmentDTO Establishment { get; set; }

        public ReservationDTO(CustomerDTO customer, DateTime startDate, DateTime endDate, CarDTO car, EstablishmentDTO establishment) {
            Customer = customer;
            StartDate = startDate;
            EndDate = endDate;
            Car = car;
            Establishment = establishment;
        }

        public override string? ToString() {
            return $"Customer: {Customer.FirstName} {Customer.LastName}\n" + 
                   $"Estabishment: {Establishment.Airport}\n" +
                   $"StartDate: {StartDate.ToString("dd/MM/yyyy")}\n" +
                   $"EndDate: {EndDate.ToString("dd/MM/yyyy")}\n" +
                   $"Car Brand: {Car.Brand}\n";
        }
    }
}
