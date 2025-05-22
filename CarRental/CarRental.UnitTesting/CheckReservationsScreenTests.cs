using CarRental.Domain;
using CarRental.Domain.DTOs;
using CarRental.UnitTesting.Helpers;

namespace CarRental.UnitTesting {
    public class CheckReservationsScreenTests {
        [Fact]
        public void GetFilterdReservations_ByNameAndDateAndEstablishment_ReturnsCorrect() {
            #region Arrange

            DomainManager domainManager = TestDomainFactory.CreateDomainManagerWithDummies();
            var date = new DateTime(2025, 6, 23);
            var establishment = new EstablishmentDTO(
                1,
                "TestEstablishment",
                "straat",
                "9160",
                "Lokeren",
                "Belgium"
                );

            #endregion
            #region Act

            var result = domainManager.GetFilterdReservations("Test", date, establishment);

            #endregion
            #region Assert

            Assert.All(result, r => {
                Assert.Contains("Test", (r.Customer.FirstName + " " + r.Customer.LastName).ToLower());
                Assert.True(r.StartDate.Date <= date && r.EndDate.Date >= date);
                Assert.Equal(establishment.Id, r.Establishment.Id);
            });

            #endregion
        }
    }
}
