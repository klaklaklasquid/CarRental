using CarRental.Domain;
using CarRental.UnitTesting.Helpers;

namespace CarRental.UnitTesting {
    public class CreateReservationScreenTests {
        [Fact]
        public void GetFilterdCarSeats_WithStateTrue_ReturnsOnlyMatchingSeats() {
            #region Arrange

            DomainManager domainManager = TestDomainFactory.CreateDomainManagerWithDummies();

            #endregion
            #region Act

            var filtered = domainManager.GetFilterdCarSeats(true, 1, 5);

            #endregion
            #region Assert

            Assert.All(filtered, car => Assert.Equal(5, car.Seats));

            #endregion
        }

        [Fact]
        public void GetFilterdCarSeats_WithStateFalse_ReturnsAllCars() {
            #region Arrange

            DomainManager domainManager = TestDomainFactory.CreateDomainManagerWithDummies();

            #endregion
            #region Act

            var expected = domainManager.GetCarByAirportId(1);
            var actual = domainManager.GetFilterdCarSeats(false, 1, 0);

            #endregion
            #region Assert

            Assert.Equal(expected.Count, actual.Count());

            #endregion
        }
    }
}
