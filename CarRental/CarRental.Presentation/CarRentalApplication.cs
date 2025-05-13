
using CarRental.Domain;
using CarRental.Presentation.Windows;

namespace CarRental.Presentation {
    public class CarRentalApplication {
        private readonly DomainManager _domainManager;
        private readonly MainWindow _mainWindow;

        public CarRentalApplication(DomainManager domainManager) {
            _domainManager = domainManager;

            _mainWindow = new MainWindow();
            _mainWindow.Show();
        }
    }
}
