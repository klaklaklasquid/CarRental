using CarRental.Domain.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarRental.Persistence.Mapper {
    public class CustomerMapper : ICustomerRepository {
        private SqlConnection _connection;

        public CustomerMapper() {
            _connection = new SqlConnection(DbInfo.ConnectionString);
        }

        public void WipeDatabase() {
            try {
                _connection.Open();
                using SqlCommand command = new("DELETE FROM Klanten", _connection);
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
            string errorFilePath = Path.Combine(folderPath, "foutbestandCustomers.csv");

            try {
                _connection.Open();

                foreach (string line in lines.Skip(1)) {
                    string[] values = line.Split(';');

                    if (values.Length < 7) {
                        errorLines.Add(line);
                        continue;
                    }

                    string firstName = values[0].Trim();
                    string lastName = values[1].Trim();
                    string email = values[2].Trim();
                    string street = values[3].Trim();
                    string zipcode = values[4].Trim();
                    string city = values[5].Trim();
                    string country = values[6].Trim();

                    // validation rules
                    bool isFilledIn =
                        !string.IsNullOrWhiteSpace(firstName) &&
                        !string.IsNullOrWhiteSpace(lastName) &&
                        !string.IsNullOrWhiteSpace(email) &&
                        !string.IsNullOrWhiteSpace(street) &&
                        !string.IsNullOrWhiteSpace(zipcode) &&
                        !string.IsNullOrWhiteSpace(city) &&
                        !string.IsNullOrWhiteSpace(country) &&
                        zipcode.Any(char.IsDigit) &&
                        street.Any(char.IsDigit) &&
                        !firstName.Any(char.IsDigit) &&
                        !lastName.Any(char.IsDigit) &&
                        !city.Any(char.IsDigit) &&
                        !country.Any(char.IsDigit);

                    // email validation
                    string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    bool isValidEmail = Regex.IsMatch(email, pattern);

                    bool isValid =
                        isFilledIn &&
                        isValidEmail &&
                        !seenEntries.Contains(email.ToLower());

                    if (!isValid) {
                        errorLines.Add(line);
                        continue;
                    }

                    // check for duplicates
                    string entry = $"{firstName};{lastName};{email};{street};{zipcode};{city};{country}";
                    if (seenEntries.Contains(entry)) {
                        errorLines.Add(line);
                        continue;
                    }
                    seenEntries.Add(entry);

                    // insert into database
                    using SqlCommand command =
                        new("INSERT INTO Klanten (Voornaam, Achternaam, Email, Straat, Postcode, Woonplaats, Land) VALUES (@firstName, @lastName, @email, @street, @zipcode, @city, @country)", _connection);
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@street", street);
                    command.Parameters.AddWithValue("@zipcode", zipcode);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@country", country);
                    command.ExecuteNonQuery();

                }
            } catch (Exception ex) {
                throw new Exception("Error while inserting Customers into the database: " + ex.Message, ex);
            } finally {
                _connection.Close();

                // write error log file
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
