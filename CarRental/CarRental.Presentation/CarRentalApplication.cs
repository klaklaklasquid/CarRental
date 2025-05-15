
using CarRental.Domain;
using CarRental.Domain.Sevices;
using CarRental.Presentation.Windows;

namespace CarRental.Presentation {
    public class CarRentalApplication {
        private readonly DomainManager _domainManager;
        private readonly UserContext _context = new();

        private readonly MainWindow _mainWindow;
        private readonly ChooseOptionWindow _chooseOptionWindow;

        public CarRentalApplication(DomainManager domainManager) {
            _domainManager = domainManager;

            _mainWindow = new MainWindow(this, _domainManager);
            _chooseOptionWindow = new ChooseOptionWindow(_domainManager);

            _mainWindow.Show();
        }

        public void ChangeWindow(Object window) {
            if (window is MainWindow) {
                _chooseOptionWindow.Show();
                _mainWindow.Close();
            } else if (window is ChooseOptionWindow) {
                _mainWindow.Show();
                _chooseOptionWindow.Close();
            }
        }
    }
}
