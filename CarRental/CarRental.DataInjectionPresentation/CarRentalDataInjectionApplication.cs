using CarRental.DataInjectionPresentation.Windows;
using CarRental.Domain;

namespace CarRental.DataInjectionPresentation {
    public class CarRentalDataInjectionApplication {
        private readonly DomainManager _domainManager;

        private readonly InjectDataWindow _injectDataWindow;

        public CarRentalDataInjectionApplication(DomainManager domainManager) {
            _domainManager = domainManager;

            _injectDataWindow = new InjectDataWindow();
            _injectDataWindow.Show();
        }
    }
}

