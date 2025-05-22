using CarRental.Domain.DTOs;
using CarRental.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.UnitTesting.DummyMappers {
    class DummyCarMapper : ICarRepository {
        public List<CarDTO> GetCarByAirportId(int id) {
            List<CarDTO> cars = new();

            cars.Add(new CarDTO(1, "abc-de-001", "notCar", 5, "Diesel", id));
            cars.Add(new CarDTO(2, "xyz-ab-002", "testCar", 4, "Petrol", id));
            cars.Add(new CarDTO(3, "lmn-xy-003", "sampleCar", 2, "Electric", id));
            cars.Add(new CarDTO(4, "pqr-uv-004", "mockCar", 7, "Hybrid", id));
            cars.Add(new CarDTO(5, "stu-wx-005", "dummyCar", 5, "Diesel", id));

            // Add 5 more with id + 1
            cars.Add(new CarDTO(6, "abc-de-101", "notCarPlus", 5, "Diesel", id + 1));
            cars.Add(new CarDTO(7, "xyz-ab-102", "testCarPlus", 4, "Petrol", id + 1));
            cars.Add(new CarDTO(8, "lmn-xy-103", "sampleCarPlus", 2, "Electric", id + 1));
            cars.Add(new CarDTO(9, "pqr-uv-104", "mockCarPlus", 7, "Hybrid", id + 1));
            cars.Add(new CarDTO(10, "stu-wx-105", "dummyCarPlus", 5, "Diesel", id + 1));

            return cars;
        }

        public void InitData(string csvFile) {
            throw new NotImplementedException();
        }

        public void WipeDatabase() {
            throw new NotImplementedException();
        }
    }
}
