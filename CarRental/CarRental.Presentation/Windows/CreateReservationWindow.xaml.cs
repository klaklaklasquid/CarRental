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

        public CreateReservationWindow(CarRentalApplication application) {
            InitializeComponent();
            _application = application;

            establishmentsListName.ItemsSource = _application.GetEstablishments();
        }

        private void HandleChangeList(object sender, SelectionChangedEventArgs e) {
            int airportId = ((EstablishmentDTO)establishmentsListName.SelectedItem).Id;
            carsListName.ItemsSource = _application.GetCarByAirportId(airportId);
        }

        public void SetPlaceholderName(string placeholderName) {
            placeholderForName.Text = placeholderName;
        }

        public void SetPlaceholderAirport(string placeholderAirport) {
            placeholderForAirport.Text = placeholderAirport;
        }

    }
}
