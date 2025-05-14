using CarRental.Domain.Model;
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

                int lineNumber = 2; // Start at 2 because Skip(1) skips the header (line 1)
                foreach (string line in lines.Skip(1)) {
                    string[] values = line.Split(';');

                    if (values.Length < 7) {
                        errorLines.Add($"{lineNumber};{line};Error: Not enough columns");
                        lineNumber++;
                        continue;
                    }

                    // for ease of use, trim the values
                    string firstName = values[0].Trim();
                    string lastName = values[1].Trim();
                    string email = values[2].Trim();
                    string street = values[3].Trim();
                    string zipcode = values[4].Trim();
                    string city = values[5].Trim();
                    string country = values[6].Trim();

                    // check for duplicates
                    string entry = $"{firstName};{lastName};{email};{street};{zipcode};{city};{country}";
                    if (seenEntries.Contains(entry)) {
                        errorLines.Add($"{lineNumber};{line};Error: Duplicate entry");
                        lineNumber++;
                        continue;
                    }
                    seenEntries.Add(entry);

                    // make object and then insert into database
                    try {
                        Customer customer = new(firstName, lastName, email, street, zipcode, city, country);


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

                    } catch (Exception ex) {
                        errorLines.Add($"{lineNumber};{line};Error: {ex.Message}");
                        lineNumber++;
                        continue;
                    }
                    lineNumber++;
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
