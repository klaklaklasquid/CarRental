using CarRental.Domain.DTOs;
using CarRental.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.UnitTesting.DummyMappers {
    class DummyCustomerMapper : ICustomerRepository {
        public List<CustomerDTO> GetCustomers() {
            List<CustomerDTO> customers = new();

            customers.Add(new CustomerDTO("Jordy", "Van Belle", "JordyNotReal@mail.com", "Kerkstraat 1", "1234AB", "Amsterdam", "Nederland"));
            customers.Add(new CustomerDTO("Dean", "Rogiest", "deanNotReal@mail.com", "Kerkstraat 2", "1234AB", "Amsterdam", "Nederland"));
            customers.Add(new CustomerDTO("Sophie", "Jansen", "sophie.jansen@mail.com", "Lindelaan 5", "5678CD", "Rotterdam", "Nederland"));
            customers.Add(new CustomerDTO("Lucas", "De Vries", "lucas.devries@mail.com", "Stationsweg 10", "4321EF", "Utrecht", "Nederland"));
            customers.Add(new CustomerDTO("Emma", "Bakker", "emma.bakker@mail.com", "Parkstraat 8", "8765GH", "Den Haag", "Nederland"));

            return customers;
        }

        public void InitData(string csvFile) {
            throw new NotImplementedException();
        }

        public void WipeDatabase() {
            throw new NotImplementedException();
        }
    }
}
