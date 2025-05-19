using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Model {
    public class Reservation {
        private int _id;

        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        private Customer _customer;

        public Customer Customer {
            get { return _customer; }
            set { _customer = value; }
        }

        private DateTime _startTime;

        public DateTime StartTime {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private DateTime _endTime;

        public DateTime EndTime {
            get { return _endTime; }
            set { _endTime = value; }
        }

        private Car _car;

        public Car Car {
            get { return  _car; }
            set {  _car = value; }
        }


        public Reservation(Customer customer, DateTime startTime, DateTime endTime, Car car) {
            Customer = customer;
            StartTime = startTime;
            EndTime = endTime;
            Car = car;
        }

        public Reservation(int id, Customer customer, DateTime startTime, DateTime endTime, Car car) : this(customer, startTime, endTime, car) {
            Id = id;
        }

        public override bool Equals(object? obj) {
            return obj is Reservation reservation &&
                   Id == reservation.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }
    }
}
