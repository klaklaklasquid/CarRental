using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.DTOs {
    public class ReservationDTO {
        public string CustomerEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CarLicensePlate { get; set; }

        public ReservationDTO(string customerEmail, DateTime startDate, DateTime endDate, string carLicensePlate) {
            CustomerEmail = customerEmail;
            StartDate = startDate;
            EndDate = endDate;
            CarLicensePlate = carLicensePlate;
        }
    }
}
