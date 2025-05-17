using CarRental.Domain.DTOs;
using CarRental.Domain.Repository;
using Microsoft.Win32;


namespace CarRental.Domain {
    public class DomainManager {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEstablishmentRepository _establishmentRepository;

        public DomainManager(ICarRepository carRepository, ICustomerRepository customerRepository, IEstablishmentRepository establishmentRepository) {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _establishmentRepository = establishmentRepository;
        }

        public void WipeAll() {
            _establishmentRepository.WipeDatabase();
            _customerRepository.WipeDatabase();
            _carRepository.WipeDatabase();
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
    }
}
