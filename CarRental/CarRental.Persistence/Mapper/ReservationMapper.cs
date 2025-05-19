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
                using SqlCommand command = new("SELECT * from Reservaties r join Klanten k on r.klantEmail = k.email join Autos a on r.autoNummerplaat = a.Nummerplaat", _connection);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        // create customer
                        string firstName = (string)reader["Voornaam"];
                        string lastName = (string)reader["Achternaam"];
                        string email = (string)reader["Email"];
                        string Zipcode = (string)reader["Postcode"];
                        string street = (string)reader["Straat"];
                        string city = (string)reader["Woonplaats"];
                        string country = (string)reader["Land"];

                        CustomerDTO customer = new(firstName, lastName, email, Zipcode, street, city, country);

                        // create car
                        int id = (int)reader["Id"];
                        string licensePlate = (string)reader["Nummerplaat"];
                        string brand = (string)reader["Model"];
                        int seats = (int)reader["Zitplaatsen"];
                        string EngineType = (string)reader["Motortype"];
                        int airportId = (int)reader["Luchthaven_id"];

                        CarDTO car = new(id, licensePlate, brand, seats, EngineType, airportId);

                        // create reservation
                        DateTime startDate = (DateTime)reader["StartDatum"];
                        DateTime endDate = (DateTime)reader["EindDatum"];

                        ReservationDTO reservation = new(customer, startDate, endDate, car);
                        reservations.Add(reservation);
                    }
                }
                return reservations;
            } finally {
                _connection.Close();
            }
        }
    }
}
