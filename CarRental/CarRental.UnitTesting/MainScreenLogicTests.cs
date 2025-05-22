using CarRental.Domain;
using CarRental.Domain.DTOs;
using CarRental.Domain.Repository;
using CarRental.UnitTesting.DummyMappers;
using CarRental.UnitTesting.Helpers;

namespace CarRental.UnitTesting {
    public class MainScreenLogicTests {
        [Fact]
        public void GetFilterUserMainScreen_WithEmptyFilter_ReturnsAllCustomers() {
            #region Arrange

            DomainManager domainManager = TestDomainFactory.CreateDomainManagerWithDummies();
            string filter = string.Empty;

            #endregion
            #region Act

            var all = domainManager.GetFilterUserMainScreen(filter);

            #endregion
            #region Assert

            Assert.Equal(domainManager.GetCustomers().Count(), all.Count());

            #endregion
        }

        [Fact]
        public void GetFilterUserMainScreen_WithNonEmptyFilter_ReturnsFilteredCustomers() {
            #region Arrange

            DomainManager domainManager = TestDomainFactory.CreateDomainManagerWithDummies();
            string filter = "Jordy";

            #endregion
            #region Act

            var filtered = domainManager.GetFilterUserMainScreen(filter);

            #endregion
            #region Assert

            Assert.All(filtered, customer => Assert.StartsWith(filter, $"{customer.FirstName} {customer.LastName}", StringComparison.CurrentCultureIgnoreCase));

            #endregion
        }
    }
}
