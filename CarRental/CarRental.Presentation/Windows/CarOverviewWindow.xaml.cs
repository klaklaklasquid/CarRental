using CarRental.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRental.Presentation.Windows {
    /// <summary>
    /// Interaction logic for CarOverviewWindow.xaml
    /// </summary>
    public partial class CarOverviewWindow : Window {
        private readonly CarRentalApplication _application;

        public CarOverviewWindow(CarRentalApplication application) {
            InitializeComponent();
            _application = application;
            listViewEstablishment.ItemsSource = _application.GetEstablishments();
        }

        private void listViewEstablishment_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            FilterCars();
        }

        private void filterDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            FilterCars();
        }

        private void FilterCars() {
            // Get selected establishment
            EstablishmentDTO selectedEstablishment = (EstablishmentDTO)listViewEstablishment.SelectedItem;
            if (selectedEstablishment == null) {
                listViewCars.ItemsSource = null;
                return;
            }

            // Get selected date
            var selectedDate = filterDate.SelectedDate;
            if (selectedDate == null) {
                listViewCars.ItemsSource = null;
                return;
            }

            // Get all cars for the selected establishment
            var cars = _application.GetCarByAirportId(selectedEstablishment.Id);

            // Get all reservations
            var reservations = _application.GetReservations();

            // Filter cars that are available on the selected date
            var availableCars = cars.Where(car =>
                !reservations.Any(res =>
                    res.Car != null &&
                    res.Car.Id == car.Id &&
                    selectedDate >= res.StartDate.Date &&
                    selectedDate <= res.EndDate.Date
                )
            ).ToList();

            listViewCars.ItemsSource = availableCars;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            // Get selected establishment
            EstablishmentDTO selectedEstablishment = (EstablishmentDTO)listViewEstablishment.SelectedItem;
            if (selectedEstablishment == null) {
                MessageBox.Show("Please select an establishment.");
                return;
            }

            // Get selected date
            var selectedDate = filterDate.SelectedDate;
            if (selectedDate == null) {
                MessageBox.Show("Please select a date.");
                return;
            }

            // Get selected car
            CarDTO selectedCar = (CarDTO)listViewCars.SelectedItem;
            if (selectedCar == null) {
                MessageBox.Show("Please select a car.");
                return;
            }

            // Get all reservations for this car, ordered by start date
            var reservations = _application.GetReservations()
                .Where(r => r.Car != null && r.Car.Id == selectedCar.Id)
                .OrderBy(r => r.StartDate)
                .ToList();

            // Find previous and next reservation relative to selectedDate
            ReservationDTO? previousReservation = reservations
                .Where(r => r.EndDate.Date < selectedDate.Value.Date)
                .OrderByDescending(r => r.EndDate)
                .FirstOrDefault();

            ReservationDTO? nextReservation = reservations
                .Where(r => r.StartDate.Date > selectedDate.Value.Date)
                .OrderBy(r => r.StartDate)
                .FirstOrDefault();

            // Markdown generation
            var sb = new StringBuilder();
            sb.AppendLine("# Overzicht auto's\n");
            sb.AppendLine($"**Vestiging:** {selectedEstablishment.Airport}");
            sb.AppendLine($"**Date:** {selectedDate.Value:yyyy-MM-dd}\n");
            sb.AppendLine($"## {selectedCar.LicensePlate} - {selectedCar}\n");

            sb.AppendLine("### vorige reservatie:");
            if (previousReservation != null) {
                var customer = previousReservation.Customer;
                sb.AppendLine($"**klant:** {customer?.ToString() ?? "Onbekend"}");
                sb.AppendLine($"**Straat:** {customer?.Street ?? "Onbekend"}");
                sb.AppendLine($"**Postcode:** {customer?.Zipcode ?? "Onbekend"}");
                sb.AppendLine($"**Stad:** {customer?.City ?? "Onbekend"}");
                sb.AppendLine($"**Land:** {customer?.Country ?? "Onbekend"}");
                sb.AppendLine($"**periode:** {previousReservation.StartDate:yyyy-MM-dd} t/m {previousReservation.EndDate:yyyy-MM-dd}");
            } else {
                sb.AppendLine("Geen vorige reservatie.");
                sb.AppendLine("No previous reservation.");
            }

            sb.AppendLine("\n### volgende reservatie:");
            if (nextReservation != null) {
                var customer = nextReservation.Customer;
                sb.AppendLine($"**klant:** {customer?.ToString() ?? "Onbekend"}");
                sb.AppendLine($"**Straat:** {customer?.Street ?? "Onbekend"}");
                sb.AppendLine($"**Postcode:** {customer?.Zipcode ?? "Onbekend"}");
                sb.AppendLine($"**Stad:** {customer?.City ?? "Onbekend"}");
                sb.AppendLine($"**Land:** {customer?.Country ?? "Onbekend"}");
                sb.AppendLine($"**periode:** {nextReservation.StartDate:yyyy-MM-dd} t/m {nextReservation.EndDate:yyyy-MM-dd}");
            } else {
                sb.AppendLine("Geen volgende reservatie.");
                sb.AppendLine("No future reservation.");
            }

            // Save to file
            var dialog = new Microsoft.Win32.SaveFileDialog {
                Filter = "Markdown file (*.md)|*.md",
                FileName = $"{selectedCar.LicensePlate}_{selectedDate.Value:yyyyMMdd}.md"
            };
            if (dialog.ShowDialog() == true) {
                System.IO.File.WriteAllText(dialog.FileName, sb.ToString());
                MessageBox.Show("Markdown document saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
