using CarRental.Domain;
using CarRental.Domain.DTOs;
using CarRental.UnitTesting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.UnitTesting {
    public class MarkdownGenerationTests {
        [Fact]
        public void GenerateCarOverviewMarkdown_ReturnsMarkdown_WithPreviousAndNext() {
            DomainManager domainManager = TestDomainFactory.CreateDomainManagerWithDummies();

            var car = new CarDTO(1, "1-ABC-123", "Toyota", 2020, "Corolla", 5);
            var est = new EstablishmentDTO(1, "Zaventem", "Airportstraat 1", "1930", "Zaventem", "BE");
            var date = new DateTime(2024, 6, 15);

            var customerJane = new CustomerDTO("Jane", "Doe", "Main", "1000", "Brussel", "BE", "jane.doe@email.com");
            var customerJohn = new CustomerDTO("John", "Smith", "Second", "2000", "Antwerpen", "BE", "john.smith@email.com");

            var reservations = new List<ReservationDTO>
            {
                    new ReservationDTO(customerJane, new DateTime(2024, 6, 10), new DateTime(2024, 6, 12), car, est),
                    new ReservationDTO(customerJohn, new DateTime(2024, 6, 20), new DateTime(2024, 6, 22), car, est)
                };

            // Assuming the service is the DomainManager or a property of it
            var result = domainManager.GenerateCarOverviewMarkdown(est, date, car, reservations);

            Assert.Contains("## 1-ABC-123", result);
            Assert.Contains("vorige reservatie", result.ToLower());
            Assert.Contains("volgende reservatie", result.ToLower());
            Assert.Contains("jane", result.ToLower());
            Assert.Contains("john", result.ToLower());
        }
    }
}
