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
    /// Interaction logic for CreateReservationWindow.xaml
    /// </summary>
    public partial class CreateReservationWindow : Window {
        private readonly CarRentalApplication _application;
        private int _airportId;
        private int _seats = 2;
        private bool _isChecked = false;

        public CreateReservationWindow(CarRentalApplication application) {
            InitializeComponent();
            _application = application;

            establishmentsListName.ItemsSource = _application.GetEstablishments();
        }

        private void HandleChangeList(object sender, SelectionChangedEventArgs e) {
            _airportId = ((EstablishmentDTO)establishmentsListName.SelectedItem).Id;
            carsListName.ItemsSource = _application.GetCarByAirportId(_airportId);
            if (establishmentsListName.SelectedItem != null) {
                placeholderForAirport.Text = ((EstablishmentDTO)establishmentsListName.SelectedItem).Airport;
                placeholderForCar.Text = string.Empty;
            }
        }

        private void HandleChangeCarList(object sender, SelectionChangedEventArgs e) {
            if(!carsListName.HasItems) {
                carListError.Opacity = 1;
            }

            if (carsListName.SelectedItem != null) {
                placeholderForCar.Text = ((CarDTO)carsListName.SelectedItem).Brand;
            }
        }

        public void SetPlaceholderName(string placeholderName) {
            placeholderForName.Text = placeholderName;
        }

        private void valueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            _seats = (int)valueSlider.Value;
            if (establishmentsListName.SelectedItem != null) {
                carsListName.ItemsSource = _application.GetCarByAirportId(_airportId).Where(airport => airport.Seats == _seats);
            }
        }

        private void checkboxForSeat_Checked(object sender, RoutedEventArgs e) {
            _isChecked = true;
            if(establishmentsListName.SelectedItem != null && _isChecked) {
                stackpanelForSlider.Focusable = true;
                stackpanelForSlider.IsHitTestVisible = true;
                stackpanelForSlider.Opacity = 1;
                carsListName.ItemsSource = _application.GetCarByAirportId(_airportId).Where(airport => airport.Seats == _seats);
            }
        }

        private void checkboxForSeat_Unchecked(object sender, RoutedEventArgs e) {
            _isChecked = false;
            if (establishmentsListName.SelectedItem != null && !_isChecked) {
                stackpanelForSlider.Focusable = false;
                stackpanelForSlider.IsHitTestVisible= false;
                stackpanelForSlider.Opacity = 0.3;
                carsListName.ItemsSource = _application.GetCarByAirportId(_airportId);
            }
        }
    }
}
