
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
        private readonly CarOverviewWindow _carOverviewWindow;

        public CustomerDTO User { get; set; }

        private Dictionary<string, Window> _windowsByTag;

        public CarRentalApplication(DomainManager domainManager) {
            _domainManager = domainManager;

            _mainWindow = new MainWindow(this);
            _chooseOptionWindow = new ChooseOptionWindow(this);
            _createReservationWindow = new CreateReservationWindow(this);
            _checkReservationsWindow = new CheckReservationsWindow(this);
            _carOverviewWindow = new CarOverviewWindow(this);

            _mainWindow.Show();

            _windowsByTag = new() {
                {"1", _createReservationWindow},
                {"2", _checkReservationsWindow},
                {"3", _carOverviewWindow}
            };

            _chooseOptionWindow.OpenWindowRequested += OpenWindows;
        }

        private void OpenWindows(object? sender, string e) {
            _windowsByTag.TryGetValue(e, out Window? window);
            if (window != null) {
                _createReservationWindow.GetCustomer(User);
                _checkReservationsWindow.GetCustomer(User);
                window.Show();
            }
        }

        public void ChangeWindow(Object window, CustomerDTO ctr) {
            if (window is MainWindow) {
                User = ctr;
                _chooseOptionWindow.GetCustomer(User);
                _chooseOptionWindow.Show();
                _mainWindow.Hide();
            }
        }

        public List<EstablishmentDTO> GetEstablishments() {
            return _domainManager.GetEstablishments();
        }

        public List<CustomerDTO> GetCustomers() {
            return _domainManager.GetCustomers();
        }

        public List<CarDTO> GetCarByAirportId(int id) {
            return _domainManager.GetCarByAirportId(id);
        }

        public void SetReservation(Reservation reservation) {
            _domainManager.SetReservation(reservation);
        }

        public List<ReservationDTO> GetReservations() {
            return _domainManager.GetReservations();
        }

        public void DeleteReservation(ReservationDTO reservation) {
            _domainManager.DeleteReservation(reservation);
        }

        #region MainScreen Logic

        public IEnumerable<CustomerDTO> GetFilterUserMainScreen(string filter) {
            return _domainManager.GetFilterUserMainScreen(filter);
        }

        #endregion

        #region CreateReservationScreen Logic

        public IEnumerable<CarDTO> GetFilterdCarSeats(bool state, int id, int seats) {
            return _domainManager.GetFilterdCarSeats(state, id, seats);
        }

        #endregion

        #region CheckReservationsScreen Logic

        public List<ReservationDTO> GetFilterdReservations(string name, DateTime? date, EstablishmentDTO establishment) {
            return _domainManager.GetFilterdReservations(name, date, establishment);
        }

        #endregion

        #region CarOverviewScreen Logic

        public List<CarDTO> GetFilterdCars(EstablishmentDTO establishment, DateTime? date) {
            return _domainManager.GetFilterdCars(establishment, date);
        }

        public string GenerateCarOverviewMarkdown(
            EstablishmentDTO establishment,
            DateTime date,
            CarDTO car,
            List<ReservationDTO> reservations) {
            return _domainManager.GenerateCarOverviewMarkdown(establishment, date, car, reservations);
        }

        #endregion
    }
}
