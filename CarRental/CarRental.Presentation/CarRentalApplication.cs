
using CarRental.Domain;
using CarRental.Domain.DTOs;
using CarRental.Presentation.Windows;

namespace CarRental.Presentation {
    public class CarRentalApplication {
        private readonly DomainManager _domainManager;

        private readonly MainWindow _mainWindow;
        private readonly ChooseOptionWindow _chooseOptionWindow;

        public CarRentalApplication(DomainManager domainManager) {
            _domainManager = domainManager;

            _mainWindow = new MainWindow(this, _domainManager);
            _chooseOptionWindow = new ChooseOptionWindow(_domainManager);

            _mainWindow.Show();
        }

        public void ChangeWindow(Object window, CustomerDTO ctr) {
            if (window is MainWindow) {
                _chooseOptionWindow.SetSelectedName(ctr.FirstName);
                _chooseOptionWindow.Show();
                _mainWindow.Close();
            } else if (window is ChooseOptionWindow) {
                _mainWindow.Show();
                _chooseOptionWindow.Close();
            }
        }
    }
}
