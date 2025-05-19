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
                command.Parameters.AddWithValue("@KlantEmail", reservation.CustomerEmail);
                command.Parameters.AddWithValue("@StartDatum", reservation.StartTime);
                command.Parameters.AddWithValue("@EindDatum", reservation.EndTime);
                command.Parameters.AddWithValue("@AutoNummerPlaat", reservation.CarLicensePlate);
                command.ExecuteNonQuery();
            } finally {
                _connection.Close();
            }
        }

        public List<ReservationDTO> GetReservations() {
            try {
                List<ReservationDTO> reservations = new();
                _connection.Open();
                using SqlCommand command = new("SELECT * FROM Reservaties", _connection);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        string email = (string)reader["KlantEmail"];
                        DateTime startDate = (DateTime)reader["StartDatum"];
                        DateTime endDate = (DateTime)reader["EindDatum"];
                        string licensePlate = (string)reader["AutoNummerplaat"];

                        reservations.Add(new ReservationDTO(email, startDate, endDate, licensePlate));
                    }
                }
                return reservations;
            } finally {
                _connection.Close();
            }
        }
    }
}
