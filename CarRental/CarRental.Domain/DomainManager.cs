﻿using CarRental.Domain.DTOs;
using CarRental.Domain.Model;
using CarRental.Domain.Repository;
using Microsoft.Win32;
using System.Text;


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

        public void DeleteReservation(ReservationDTO reservation) {
            Reservation res = new(reservation);
            _reservationRepository.DeleteReservation(res);
        }

        #region MainScreen Logic

        // filter for username
        public IEnumerable<CustomerDTO> GetFilterUserMainScreen(string filter) {
            return string.IsNullOrWhiteSpace(filter) ?
                new List<CustomerDTO>(GetCustomers()) :
                new List<CustomerDTO>(GetCustomers()).Where(c => {
                    string fullName = $"{c.FirstName} {c.LastName}";
                    return fullName.StartsWith(filter, StringComparison.CurrentCultureIgnoreCase);
                });
        }

        #endregion

        #region CreateReservationScreen Logic

        // filter for which cars get shown according to establishment id
        public IEnumerable<CarDTO> GetFilterdCarSeats(bool state, int id, int seats) {
            IEnumerable<CarDTO> cars;
            if (state) {
                cars = GetCarByAirportId(id).Where(c => c.Seats == seats);
            } else {
                cars = GetCarByAirportId(id);
            }
            return cars;
        }

        #endregion

        #region CheckReservationsScreen Logic

        // filter for which reservations get shown according to the filters
        public List<ReservationDTO> GetFilterdReservations(string name, DateTime? date, EstablishmentDTO establishment) {
            List<ReservationDTO> filtered = GetReservations().Where(r =>
                (string.IsNullOrEmpty(name) ||
                    (r.Customer.FirstName + " " + r.Customer.LastName).ToLower().Contains(name)) &&
                (!date.HasValue ||
                    (r.StartDate.Date <= date.Value.Date && r.EndDate.Date >= date.Value.Date)) &&
                (establishment == null ||
                    r.Establishment.Id == establishment.Id)
            ).ToList();
            return filtered;
        }

        #endregion

        #region CarOverviewScreen Logic

        // returns list of filterd cars
        public List<CarDTO> GetFilterdCars(EstablishmentDTO establishment, DateTime? date) {
            List<CarDTO> cars = GetCarByAirportId(establishment.Id);
            List<ReservationDTO> reservations = GetReservations();

            return cars.Where(car =>
                !reservations.Any(res =>
                    res.Car != null &&
                    res.Car.Id == car.Id &&
                    date >= res.StartDate.Date &&
                    date <= res.EndDate.Date
                    )
                ).ToList();
        }

        public string GenerateCarOverviewMarkdown(
            EstablishmentDTO establishment,
            DateTime date,
            CarDTO car,
            List<ReservationDTO> reservations) {
            // Find previous and next reservation relative to date
            var previousReservation = reservations
                .Where(r => r.EndDate.Date < date.Date)
                .OrderByDescending(r => r.EndDate)
                .FirstOrDefault();

            var nextReservation = reservations
                .Where(r => r.StartDate.Date > date.Date)
                .OrderBy(r => r.StartDate)
                .FirstOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine("# Overzicht auto's\n");
            sb.AppendLine($"**Vestiging:** {establishment.Airport}");
            sb.AppendLine($"**Date:** {date:yyyy-MM-dd}\n");
            sb.AppendLine($"## {car.LicensePlate} - {car}\n");

            sb.AppendLine("### vorige reservatie:");
            if (previousReservation != null) {
                var customer = previousReservation.Customer;
                sb.AppendLine($"**klant:** {customer?.ToString() ?? "Onbekend"}");
                sb.AppendLine($"**Straat:** {customer?.Street ?? "Onbekend"}");
                sb.AppendLine($"**Postcode:** {customer?.Zipcode ?? "Onbekend"}");
                sb.AppendLine($"**Stad:** {customer?.City ?? "Onbekend"}");
                sb.AppendLine($"**Land:** {customer?.Country ?? "Onbekend"}");
                sb.AppendLine($"**periode:** {previousReservation.StartDate:yyyy-MM-dd} t/m {previousReservation.EndDate:yyyy-MM-dd}");
            } else {
                sb.AppendLine("Geen vorige reservatie.");
                sb.AppendLine("No previous reservation.");
            }

            sb.AppendLine("\n### volgende reservatie:");
            if (nextReservation != null) {
                var customer = nextReservation.Customer;
                sb.AppendLine($"**klant:** {customer?.ToString() ?? "Onbekend"}");
                sb.AppendLine($"**Straat:** {customer?.Street ?? "Onbekend"}");
                sb.AppendLine($"**Postcode:** {customer?.Zipcode ?? "Onbekend"}");
                sb.AppendLine($"**Stad:** {customer?.City ?? "Onbekend"}");
                sb.AppendLine($"**Land:** {customer?.Country ?? "Onbekend"}");
                sb.AppendLine($"**periode:** {nextReservation.StartDate:yyyy-MM-dd} t/m {nextReservation.EndDate:yyyy-MM-dd}");
            } else {
                sb.AppendLine("Geen volgende reservatie.");
                sb.AppendLine("No future reservation.");
            }

            return sb.ToString();
        }

        #endregion
    }
}
