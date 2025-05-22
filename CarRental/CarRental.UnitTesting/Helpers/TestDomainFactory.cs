using CarRental.Domain;
using CarRental.Domain.Repository;
using CarRental.UnitTesting.DummyMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.UnitTesting.Helpers {
    public static class TestDomainFactory {
        public static DomainManager CreateDomainManagerWithDummies() {
            ICustomerRepository customerRepository = new DummyCustomerMapper();
            ICarRepository carRepository = new DummyCarMapper();
            IEstablishmentRepository establishmentRepository = new DummyEstablishmentMapper();
            IReservationRepository reservationRepository = new DummyReservationMapper();

            return new DomainManager(carRepository, customerRepository, establishmentRepository, reservationRepository);
        }
    }
}
