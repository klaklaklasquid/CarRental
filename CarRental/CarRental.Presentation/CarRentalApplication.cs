
using CarRental.Domain;
using CarRental.Domain.DTOs;
using CarRental.Presentation.Windows;
using System.Windows;

namespace CarRental.Presentation {
    public class CarRentalApplication {
        private readonly DomainManager _domainManager;

        private readonly MainWindow _mainWindow;
        private readonly ChooseOptionWindow _chooseOptionWindow;
        private readonly CreateReservationWindow _createReservationWindow;

        private Dictionary<string, Window> _windowsByTag;

        public CarRentalApplication(DomainManager domainManager) {
            _domainManager = domainManager;

            _mainWindow = new MainWindow(this, _domainManager);
            _chooseOptionWindow = new ChooseOptionWindow(this, _domainManager);
            _createReservationWindow = new CreateReservationWindow(this);

            _mainWindow.Show();

            _windowsByTag = new() {
                {"1", _createReservationWindow}
            };

            _chooseOptionWindow.OpenWindowRequested += OpenWindows;
        }

        private void OpenWindows(object? sender, string e) {
            _windowsByTag.TryGetValue(e, out Window window);
            if (window != null) {
                window.Show();
            }
        }

        internal void OpenWindow1() {
            _createReservationWindow.Show();
        }

        public List<EstablishmentDTO> GetEstablishments() {
            return _domainManager.GetEstablishments();
        }

        public List<CarDTO> GetCarByAirportId(int id) {
            return _domainManager.GetCarByAirportId(id);
        }

        public void ChangeWindow(Object window, CustomerDTO ctr) {
            if (window is MainWindow) {
                _chooseOptionWindow.SetSelectedName(ctr.FirstName);
                _chooseOptionWindow.Show();
                _mainWindow.Close();
            }
        }
    }
}
