using CarRental.Domain.Model;
using CarRental.Domain.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Persistence.Mapper {
    public class CarMapper : ICarRepository {
        private SqlConnection _connection;

        public CarMapper() {
            _connection = new SqlConnection(DbInfo.ConnectionString);
        }

        public void WipeDatabase() {
            try {
                _connection.Open();
                using SqlCommand command = new("DELETE FROM Autos", _connection);
                command.ExecuteReader();
            } finally {
                _connection.Close();
            }

        }

        public void InitData(string csvFile) {
            string[] lines = File.ReadAllLines(csvFile);
            List<string> errorLines = new();
            HashSet<string> seenEntries = new();
            List<int> airportIds = new();

            // creating the error log file
            string? folderPath = Path.GetDirectoryName(csvFile) ?? throw new Exception("Error: folder path is null");
            string errorFilePath = Path.Combine(folderPath, "foutbestandCars.csv");

            try {
                _connection.Open();

                // getting the ids of the airports
                using (SqlCommand cmd = new("SELECT Id FROM Vestigingen", _connection))
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        airportIds.Add(reader.GetInt32(0));
                    }
                }

                if (airportIds.Count == 0) {
                    throw new Exception("Error: no airports found in the database");
                }

                int airportIndex = 0;
                int lineNumber = 2; // Start at 2 because Skip(1) skips the header (line 1)

                // check if the file is empty
                foreach (string line in lines.Skip(1)) {
                    string[] values = line.Split(';');

                    if (values.Length < 4) {
                        errorLines.Add($"{lineNumber};{line};Error: Not enough columns");
                        lineNumber++;
                        continue;
                    }

                    // for ease of use, trim the values
                    string licensePlate = values[0].Trim();
                    string brand = values[1].Trim();
                    int seats = int.Parse(values[2].Trim());
                    string engineType = values[3].Trim();

                    // check for duplicates
                    string entry = $"{licensePlate};{brand};{seats};{engineType}".ToLower();
                    if (seenEntries.Contains(entry)) {
                        errorLines.Add($"{lineNumber};{line};Error: Duplicate entry");
                        lineNumber++;
                        continue;
                    }
                    seenEntries.Add(entry);

                    // make object and then insert into database
                    try {
                        // round robin airport id
                        int assignedAirportId = airportIds[airportIndex];
                        airportIndex = (airportIndex + 1) % airportIds.Count;

                        Car car = new(licensePlate, brand, seats, engineType, assignedAirportId);

                        //insert into database
                        using SqlCommand command =
                            new("INSERT INTO Autos (Nummerplaat, Model, Zitplaatsen, Motortype, Luchthaven_id) VALUES (@licensePlate, @brand, @seats, @engineType, @airportId)", _connection);
                        command.Parameters.AddWithValue("@licensePlate", licensePlate);
                        command.Parameters.AddWithValue("@brand", brand);
                        command.Parameters.AddWithValue("@seats", seats);
                        command.Parameters.AddWithValue("@engineType", engineType);
                        command.Parameters.AddWithValue("@airportId", assignedAirportId);
                        command.ExecuteNonQuery();
                    } catch (Exception ex) {
                        errorLines.Add($"{lineNumber};{line};Error: {ex.Message}");
                        lineNumber++;
                        continue;
                    }
                    lineNumber++;
                }
            } catch (Exception ex) {
                throw new Exception("Error while inserting Cars into database:" + ex.Message, ex);
            } finally {
                _connection.Close();

                // write error lines to file
                if (errorLines.Count > 0) {
                    if (File.Exists(errorFilePath)) {
                        File.Delete(errorFilePath);
                    }
                    File.WriteAllLines(errorFilePath, errorLines);
                }
            }
        }
    }
}
