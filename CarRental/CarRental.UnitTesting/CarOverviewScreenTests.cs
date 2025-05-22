using CarRental.Domain;
using CarRental.Domain.DTOs;
using CarRental.UnitTesting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.UnitTesting {
    public class CarOverviewScreenTests {
        [Fact]
        public void GetFilterdCars_ReturnsOnlyAvailableCars() {
            #region Arrange

            DomainManager domainManager = TestDomainFactory.CreateDomainManagerWithDummies();
            var establishment = new EstablishmentDTO(
                1,
                "TestEstablishment",
                "straat",
                "9160",
                "Lokeren",
                "Belgium"
                );
            DateTime date = new DateTime(2025, 6, 23);

            #endregion
            #region Act

            var available = domainManager.GetFilterdCars(establishment, date);
            var reservations = domainManager.GetReservations();

            #endregion
            #region Assert

            foreach (var car in available) {
                bool reserved = reservations.Any(r =>
                    r.Car?.Id == car.Id &&
                    date >= r.StartDate.Date &&
                    date <= r.EndDate.Date);

                Assert.False(reserved);
            }

            #endregion
        }
    }
}
