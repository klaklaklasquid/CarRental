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

        public void WipeDatabase() {
            try {
                _connection.Open();
                using SqlCommand command = new("DELETE FROM Vestigingen", _connection);
                using SqlDataReader reader = command.ExecuteReader();
            } finally {
                _connection.Close();
            }

        }
    }
}
