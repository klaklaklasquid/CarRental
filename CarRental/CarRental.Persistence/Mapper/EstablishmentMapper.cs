using CarRental.Domain.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Persistence.Mapper {
    public class EstablishmentMapper : IEstablishmentRepository {
        private SqlConnection _connection;

        public EstablishmentMapper() {
            _connection = new SqlConnection(DbInfo.ConnectionString);
        }

        private bool ContainsDigits(string str) => str.Any(char.IsDigit);

        public void WipeDatabase() {
            try {
                _connection.Open();
                using SqlCommand command = new("DELETE FROM Vestigingen", _connection);
                command.ExecuteReader();
            } finally {
                _connection.Close();
            }

        }

        public void InitData(string csvFile) {
            string[] lines = File.ReadAllLines(csvFile);
            List<string> errorLines = new();
            HashSet<string> seenEntries = new();

            // creating the error log file
            string? folderPath = Path.GetDirectoryName(csvFile) ?? throw new Exception("Error: folder path is null");
            string errorFilePath = Path.Combine(folderPath, "foutbestandEstablishments.csv");

            try {
                _connection.Open();

                foreach (string line in lines.Skip(1)) {
                    string[] values = line.Split(';');

                    if (values.Length < 5) {
                        errorLines.Add(line);
                        continue;
                    }

                    string airport = values[0].Trim();
                    string street = values[1].Trim();
                    string zipcode = values[2].Trim();
                    string city = values[3].Trim();
                    string country = values[4].Trim();

                    // validation rules
                    bool isValid =
                        !string.IsNullOrWhiteSpace(airport) &&
                        !string.IsNullOrWhiteSpace(street) &&
                        !string.IsNullOrWhiteSpace(zipcode) &&
                        !string.IsNullOrWhiteSpace(city) &&
                        !string.IsNullOrWhiteSpace(country) &&
                        zipcode.Any(char.IsDigit) &&
                        !ContainsDigits(airport) &&
                        !ContainsDigits(city) &&
                        !ContainsDigits(country);

                    if (!isValid) {
                        errorLines.Add(line);
                        continue;
                    }

                    // check for duplicates
                    string entry = $"{airport};{street};{zipcode};{city};{country}".ToLower();
                    if (seenEntries.Contains(entry)) {
                        errorLines.Add(line);
                        continue;
                    }
                    seenEntries.Add(entry);

                    // insert into database
                    using SqlCommand command =
                        new("INSERT INTO Vestigingen (Luchthaven, Straat, Postcode, Plaats, Land) VALUES (@airport, @street, @zipcode, @city, @country)", _connection);
                    command.Parameters.AddWithValue("@airport", airport);
                    command.Parameters.AddWithValue("@street", street);
                    command.Parameters.AddWithValue("@zipcode", zipcode);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@country", country);
                    command.ExecuteNonQuery();
                }

            } catch (Exception ex) {
                throw new Exception("Error while inserting Establishments into the database: " + ex.Message, ex);
            } finally {
                _connection.Close();

                // write error file if needed
                if (errorLines.Count > 0) {
                    if(File.Exists(errorFilePath)) {
                        File.Delete(errorFilePath);
                    }

                    File.WriteAllLines(errorFilePath, errorLines);
                }
            }
        }
    }
}
