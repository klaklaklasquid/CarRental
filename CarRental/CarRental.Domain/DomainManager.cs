using CarRental.Domain.DTOs;
using CarRental.Domain.Model;
using CarRental.Domain.Repository;
using Microsoft.Win32;


namespace CarRental.Domain {
    public class DomainManager {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEstablishmentRepository _establishmentRepository;
        private readonly IReservationRepository _reservationRepository;

        public DomainManager(ICarRepository carRepository, ICustomerRepository customerRepository, IEstablishmentRepository establishmentRepository, IReservationRepository reservationRepository) {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _establishmentRepository = establishmentRepository;
            _reservationRepository = reservationRepository;
        }

        public void WipeAll() {
            _establishmentRepository.WipeDatabase();
            _customerRepository.WipeDatabase();
            _carRepository.WipeDatabase();
            _reservationRepository.WipeDatabase();
        }

        public void InitData(string establishmentCsv, string carCsv, string customerCsv) {
            _establishmentRepository.InitData(establishmentCsv);
            _carRepository.InitData(carCsv);
            _customerRepository.InitData(customerCsv);
        }

        public List<CustomerDTO> GetCustomers() {
            return _customerRepository.GetCustomers();
        }

        public List<EstablishmentDTO> GetEstablishments() {
            return _establishmentRepository.GetEstablishments();
        }

        public List<CarDTO> GetCarByAirportId(int id) {
            return _carRepository.GetCarByAirportId(id);
        }

        public List<ReservationDTO> GetReservations() {
            return _reservationRepository.GetReservations();
        }

        public void SetReservation(Reservation reservation) {
            _reservationRepository.SetReservation(reservation);
        }
    }
}
