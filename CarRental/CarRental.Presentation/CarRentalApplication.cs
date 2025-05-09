
using CarRental.Domain;
using CarRental.Presentation.Windows;

namespace CarRental.Presentation {
    public class CarRentalApplication {
        private readonly DomainManager _domainManager;

        private readonly TestWindow _testWindow;

        public CarRentalApplication(DomainManager domainManager) {
            _domainManager = domainManager;

            _testWindow = new TestWindow();
            _testWindow.Show();
        }
    }
}
