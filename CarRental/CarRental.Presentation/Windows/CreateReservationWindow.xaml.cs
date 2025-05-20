using CarRental.Domain.DTOs;
using CarRental.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.Presentation.Windows {
    /// <summary>
    /// Interaction logic for CreateReservationWindow.xaml
    /// </summary>
    public partial class CreateReservationWindow : Window {
        private readonly CarRentalApplication _application;
        private int _airportId;
        private int _seats = 2;
        private bool _isChecked = false;
        private CustomerDTO _user;

        public CreateReservationWindow(CarRentalApplication application) {
            InitializeComponent();
            _application = application;

            establishmentsListName.ItemsSource = _application.GetEstablishments();

            stackpanelForSlider.Focusable = false;
            stackpanelForSlider.IsHitTestVisible = false;
            stackpanelForSlider.Opacity = 0.3;
        }

        private void HandleChangeList(object sender, SelectionChangedEventArgs e) {
            if (establishmentsListName.SelectedItem == null) return;
            EstablishmentDTO selectedEstablishment = (EstablishmentDTO)establishmentsListName.SelectedItem;
            _airportId = selectedEstablishment.Id;

            carsListName.ItemsSource = _application.GetFilterdCarSeats(_isChecked, _airportId);

            if (!carsListName.HasItems) {
                carListError.Opacity = 1;
                carListError.Text = _isChecked
                    ? $"There are no cars with {_seats} seats available at this location."
                    : "There are currently no cars available at this location.";
            } else {
                carListError.Opacity = 0;
                carListError.Text = "";
            }

            placeholderForAirport.Text = selectedEstablishment.Airport;
            placeholderForCar.Text = string.Empty;
        }

        private void HandleChangeCarList(object sender, SelectionChangedEventArgs e) {
            if (!carsListName.HasItems) {
                carListError.Opacity = 1;
                carListError.Text = "There are currently no cars available at this location.";
            } else {
                carListError.Opacity = 0;
                carListError.Text = "";
            }

            if (carsListName.SelectedItem != null) {
                placeholderForCar.Text = ((CarDTO)carsListName.SelectedItem).Brand;
            }
        }

        public void GetCustomer(CustomerDTO user) {
            _user = user;
            placeholderForName.Text = _user.FirstName;
        }

        private void valueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            _seats = (int)valueSlider.Value;

            if (establishmentsListName.SelectedItem != null) {
                var filteredCars = _application
                    .GetCarByAirportId(_airportId)
                    .Where(car => car.Seats == _seats);

                carsListName.ItemsSource = filteredCars;

                if (!carsListName.HasItems) {
                    carListError.Opacity = 1;
                    carListError.Text = $"There are no cars with {_seats} seats available at this location.";
                } else {
                    carListError.Opacity = 0;
                    carListError.Text = "";
                }
            }
        }

        private void checkboxForSeat_Checked(object sender, RoutedEventArgs e) {
            _isChecked = true;
            HandleCarList(_isChecked);
        }

        private void checkboxForSeat_Unchecked(object sender, RoutedEventArgs e) {
            _isChecked = false;
            HandleCarList(_isChecked);
        }

        private void HandleCarList(bool isChecked) {
            if (establishmentsListName.SelectedItem == null)
                return;

            stackpanelForSlider.Focusable = isChecked;
            stackpanelForSlider.IsHitTestVisible = isChecked;
            valueSlider.IsEnabled = isChecked;
            stackpanelForSlider.Opacity = isChecked ? 1 : 0.3;

            IEnumerable<CarDTO> cars = isChecked
                ? _application.GetCarByAirportId(_airportId).Where(c => c.Seats == _seats)
                : _application.GetCarByAirportId(_airportId);

            carsListName.ItemsSource = cars;

            if (!carsListName.HasItems) {
                carListError.Opacity = 1;
                carListError.Text = isChecked
                    ? $"There are no cars with {_seats} seats available at this location."
                    : "There are currently no cars available at this location.";
            } else {
                carListError.Opacity = 0;
                carListError.Text = "";
            }
        }

        private void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            if (startDatePicker.SelectedDate.HasValue) {
                DateTime nextDay = startDatePicker.SelectedDate.Value.AddDays(1);
                endDatePicker.DisplayDateStart = nextDay;
                placeholderStartDate.Text = startDatePicker.SelectedDate.Value.ToString("dd/MM/yyyy");

                if (endDatePicker.SelectedDate < nextDay) {
                    endDatePicker.SelectedDate = null;
                    placeholderForName.Text = "";
                }
            } else {
                endDatePicker.DisplayDateStart = null;
            }
        }

        private void endDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            placeholderEndDate.Text = endDatePicker.SelectedDate.HasValue
                ? endDatePicker.SelectedDate.Value.ToString("dd/MM/yyyy")
                : string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (startDatePicker.SelectedDate.HasValue &&
                endDatePicker.SelectedDate.HasValue &&
                carsListName.SelectedItem is CarDTO selectedCar &&
                establishmentsListName.SelectedItem is EstablishmentDTO) {

                DateTime newStart = startDatePicker.SelectedDate.Value;
                DateTime newEnd = endDatePicker.SelectedDate.Value;

                // Get all reservations for this car
                var reservations = _application.GetReservations()
                    .Where(r => r.Car.LicensePlate == selectedCar.LicensePlate);

                // Check for overlap
                bool isOverlap = reservations.Any(r =>
                    newStart <= r.EndDate && newEnd >= r.StartDate
                );

                if (isOverlap) {
                    MessageBox.Show(
                        "This car is already reserved for the selected dates.",
                        "Reservation Conflict",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                    return;
                }

                // No overlap, create reservation
                _application.SetReservation(new Reservation(
                    new Customer(_user),
                    newStart,
                    newEnd,
                    new Car(selectedCar)
                ));
                this.Hide();
                establishmentsListName.SelectedItem = null;
                carsListName.SelectedItem = null;
                startDatePicker.SelectedDate = null;
                endDatePicker.SelectedDate = null;
            } else {
                MessageBox.Show(
                    "Please make sure you have selected a start date, an end date, and a car before making a reservation.",
                    "Incomplete Reservation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }

    }
}
