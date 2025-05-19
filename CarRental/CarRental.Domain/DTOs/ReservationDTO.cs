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

        public ReservationDTO(CustomerDTO customer, DateTime startDate, DateTime endDate, CarDTO car) {
            Customer = customer;
            StartDate = startDate;
            EndDate = endDate;
            Car = car;
        }
    }
}
