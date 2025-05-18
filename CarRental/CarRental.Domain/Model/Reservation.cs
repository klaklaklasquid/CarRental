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

        private string _customerEmail;

        public string CustomerEmail {
            get { return _customerEmail; }
            set { _customerEmail = value; }
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

        private string _carLicensePlate;

        public string CarLicensePlate {
            get { return _carLicensePlate; }
            set { _carLicensePlate = value; }
        }

        public Reservation(string customerEmail, DateTime startTime, DateTime endTime, string carLicensePlate) {
            CustomerEmail = customerEmail;
            StartTime = startTime;
            EndTime = endTime;
            CarLicensePlate = carLicensePlate;
        }

        public Reservation(int id, string customerEmail, DateTime startTime, DateTime endTime, string carLicensePlate) : this(customerEmail, startTime, endTime, carLicensePlate) {
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
