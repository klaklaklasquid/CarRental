﻿using CarRental.Domain.DTOs;
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
            set {
                if (value < 0)
                    throw new ArgumentException("Id must be a positive integer.", nameof(Id));
                _id = value;
            }
        }

        private Customer _customer;

        public Customer Customer {
            get { return _customer; }
            set {
                if (value == null)
                    throw new ArgumentNullException("Customer cannot be null.", nameof(Customer));
                _customer = value;
            }
        }

        private DateTime _startTime;

        public DateTime StartTime {
            get { return _startTime; }
            set {
                if (value == default)
                    throw new ArgumentException("StartTime must be a valid date.", nameof(StartTime));
                _startTime = value;
            }
        }

        private DateTime _endTime;

        public DateTime EndTime {
            get { return _endTime; }
            set {
                if (value == default)
                    throw new ArgumentException("EndTime must be a valid date.", nameof(EndTime));
                if (_startTime != default && value < _startTime)
                    throw new ArgumentException("EndTime cannot be before StartTime.", nameof(EndTime));
                _endTime = value;
            }
        }

        private Car _car;

        public Car Car {
            get { return _car; }
            set {
                if (value == null)
                    throw new ArgumentNullException("Car cannot be null.", nameof(Car));
                _car = value;
            }
        }

        private Establishment _establishment;

        public Establishment Establishment {
            get { return _establishment; }
            set { _establishment = value; }
        }

        public Reservation(Customer customer, DateTime startTime, DateTime endTime, Car car) {
            Customer = customer;
            StartTime = startTime;
            EndTime = endTime;
            Car = car;
        }

        public Reservation(ReservationDTO reservation) {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation), "ReservationDTO cannot be null.");
            Customer = new Customer(reservation.Customer);
            StartTime = reservation.StartDate;
            EndTime = reservation.EndDate;
            Car = new Car(reservation.Car);
            Establishment = new Establishment(reservation.Establishment);
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
