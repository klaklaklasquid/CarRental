using CarRental.Domain.DTOs;
using CarRental.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.UnitTesting.DummyMappers {
    class DummyEstablishmentMapper : IEstablishmentRepository {
        public List<EstablishmentDTO> GetEstablishments() {
            throw new NotImplementedException();
        }

        public void InitData(string csvFile) {
            throw new NotImplementedException();
        }

        public void WipeDatabase() {
            throw new NotImplementedException();
        }
    }
}
