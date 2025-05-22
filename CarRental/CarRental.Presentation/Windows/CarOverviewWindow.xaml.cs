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
            DateTime? selectedDate = filterDate.SelectedDate;
            if (selectedDate == null) {
                listViewCars.ItemsSource = null;
                return;
            }

            listViewCars.ItemsSource = _application.GetFilterdCars(selectedEstablishment, selectedDate);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            // Get selected establishment
            EstablishmentDTO selectedEstablishment = (EstablishmentDTO)listViewEstablishment.SelectedItem;
            if (selectedEstablishment == null) {
                MessageBox.Show("Please select an establishment.");
                return;
            }

            // Get selected date
            DateTime? selectedDate = filterDate.SelectedDate;
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

            // Save to file
            var dialog = new Microsoft.Win32.SaveFileDialog {
                Filter = "Markdown file (*.md)|*.md",
                FileName = $"{selectedCar.LicensePlate}_{selectedDate.Value:yyyyMMdd}.md"
            };
            if (dialog.ShowDialog() == true) {
                System.IO.File.WriteAllText(dialog.FileName, _application.GenerateCarOverviewMarkdown(selectedEstablishment, (DateTime)selectedDate, selectedCar, reservations));
                MessageBox.Show("Markdown document saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;   // Cancel the close
            this.Hide();       // Hide the window instead
        }
    }
}
