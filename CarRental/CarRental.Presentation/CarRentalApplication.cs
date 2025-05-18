
using CarRental.Domain;
using CarRental.Domain.DTOs;
using CarRental.Domain.Model;
using CarRental.Presentation.Windows;
using System.Windows;

namespace CarRental.Presentation {
    public class CarRentalApplication {
        private readonly DomainManager _domainManager;

        private readonly MainWindow _mainWindow;
        private readonly ChooseOptionWindow _chooseOptionWindow;
        private readonly CreateReservationWindow _createReservationWindow;
        private readonly CheckReservationsWindow _checkReservationsWindow;

        public CustomerDTO User { get; set; }

        private Dictionary<string, Window> _windowsByTag;

        public CarRentalApplication(DomainManager domainManager) {
            _domainManager = domainManager;

            _mainWindow = new MainWindow(this, _domainManager);
            _chooseOptionWindow = new ChooseOptionWindow(this, _domainManager);
            _createReservationWindow = new CreateReservationWindow(this);
            _checkReservationsWindow = new CheckReservationsWindow(this);

            _mainWindow.Show();

            _windowsByTag = new() {
                {"1", _createReservationWindow},
                {"2", _checkReservationsWindow}
            };

            _chooseOptionWindow.OpenWindowRequested += OpenWindows;
        }

        private void OpenWindows(object? sender, string e) {
            _windowsByTag.TryGetValue(e, out Window? window);
            if (window != null) {
                _createReservationWindow.GetCustomer(User);
                window.Show();
            }
        }

        public List<EstablishmentDTO> GetEstablishments() {
            return _domainManager.GetEstablishments();
        }

        public List<CarDTO> GetCarByAirportId(int id) {
            return _domainManager.GetCarByAirportId(id);
        }

        public void SetReservation(ReservationDTO reservation) {
            _domainManager.SetReservation(reservation);
        }

        public void ChangeWindow(Object window, CustomerDTO ctr) {
            if (window is MainWindow) {
                User = ctr;
                _chooseOptionWindow.GetCustomer(User);
                _chooseOptionWindow.Show();
                _mainWindow.Close();
            }
        }
    }
}
