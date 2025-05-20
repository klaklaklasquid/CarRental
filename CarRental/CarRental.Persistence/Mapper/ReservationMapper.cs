using CarRental.Domain.DTOs;
using CarRental.Domain.Model;
using CarRental.Domain.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Persistence.Mapper {
    public class ReservationMapper : IReservationRepository {
        private SqlConnection _connection;

        public ReservationMapper() {
            _connection = new(DbInfo.ConnectionString);
        }

        public void WipeDatabase() {
            try {
                _connection.Open();
                using SqlCommand command = new("DELETE FROM Reservaties", _connection);
                command.ExecuteReader();
            } finally {
                _connection.Close();
            }
        }

        public void SetReservation(Reservation reservation) {
            try {
                _connection.Open();
                using SqlCommand command = new("INSERT INTO Reservaties (KlantEmail, StartDatum, EindDatum, AutoNummerPlaat) VALUES (@KlantEmail, @StartDatum, @EindDatum, @AutoNummerPlaat)", _connection);
                command.Parameters.AddWithValue("@KlantEmail", reservation.Customer.Email);
                command.Parameters.AddWithValue("@StartDatum", reservation.StartTime);
                command.Parameters.AddWithValue("@EindDatum", reservation.EndTime);
                command.Parameters.AddWithValue("@AutoNummerPlaat", reservation.Car.LicencePlate);
                command.ExecuteNonQuery();
            } finally {
                _connection.Close();
            }
        }

        public List<ReservationDTO> GetReservations() {
            try {
                List<ReservationDTO> reservations = new();
                _connection.Open();
                using SqlCommand command = new("SELECT a.Id as CarId, k.Postcode as CustomerZipcode, k.Straat as CustomerStreet, k.Land as CustomerCountry, v.Postcode as EstablishmentZipcode, v.Straat as EstablishmentStreet, v.Land as EstablishmentCountry, v.Id as EstablishmentId, * from Reservaties r join Klanten k on r.klantEmail = k.email join Autos a on r.autoNummerplaat = a.Nummerplaat join Vestigingen v on a.Luchthaven_id = v.Id", _connection);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        // create customer
                        string firstName = (string)reader["Voornaam"];
                        string lastName = (string)reader["Achternaam"];
                        string email = (string)reader["Email"];
                        string zipcode = (string)reader["CustomerZipcode"];
                        string street = (string)reader["CustomerStreet"];
                        string city = (string)reader["Woonplaats"];
                        string country = (string)reader["CustomerCountry"];

                        CustomerDTO customer = new(firstName, lastName, email, zipcode, street, city, country);

                        // create car
                        int carId = (int)reader["CarId"];
                        string licensePlate = (string)reader["Nummerplaat"];
                        string brand = (string)reader["Model"];
                        int seats = (int)reader["Zitplaatsen"];
                        string EngineType = (string)reader["Motortype"];
                        int airportId = (int)reader["Luchthaven_id"];

                        CarDTO car = new(carId, licensePlate, brand, seats, EngineType, airportId);

                        // create Establishment
                        int establishmentId = (int)reader["EstablishmentId"];
                        string establishmentAirport = (string)reader["Luchthaven"];
                        string establishmentStreet = (string)reader["EstablishmentStreet"];
                        string establishmentZipcode = (string)reader["EstablishmentZipcode"];
                        string establishmentCity = (string)reader["Plaats"];
                        string establishmentCountry = (string)reader["EstablishmentCountry"];

                        EstablishmentDTO establishment = new(establishmentId, establishmentAirport, establishmentStreet, establishmentZipcode, establishmentCity, establishmentCountry);

                        // create reservation
                        DateTime startDate = (DateTime)reader["StartDatum"];
                        DateTime endDate = (DateTime)reader["EindDatum"];

                        ReservationDTO reservation = new(customer, startDate, endDate, car, establishment);
                        reservations.Add(reservation);
                    }
                }
                return reservations;
            } finally {
                _connection.Close();
            }
        }

        public void DeleteReservation(Reservation reservation) {
            try {
                _connection.Open();
                using SqlCommand command = new("DELETE FROM Reservaties WHERE KlantEmail = @KlantEmail AND StartDatum = @StartDatum AND EindDatum = @EindDatum AND @AutoNummerPlaat = AutoNummerplaat", _connection);
                command.Parameters.AddWithValue("@KlantEmail", reservation.Customer.Email);
                command.Parameters.AddWithValue("@StartDatum", reservation.StartTime);
                command.Parameters.AddWithValue("@EindDatum", reservation.EndTime);
                command.Parameters.AddWithValue("@AutoNummerPlaat", reservation.Car.LicencePlate);
                command.ExecuteNonQuery();
            } finally {
                _connection.Close();
            }
        }
    }
}
