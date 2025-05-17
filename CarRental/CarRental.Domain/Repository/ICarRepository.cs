using CarRental.Domain.DTOs;
using CarRental.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Repository {
    public interface ICarRepository {
        void WipeDatabase();
        void InitData(string csvFile);

        List<CarDTO> GetCarByAirportId(int id);
    }
}
