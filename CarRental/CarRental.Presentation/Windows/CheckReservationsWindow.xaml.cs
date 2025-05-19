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
    /// Interaction logic for CheckReservationsWindow.xaml
    /// </summary>
    public partial class CheckReservationsWindow : Window {
        private readonly CarRentalApplication _application;
        private readonly List<CustomerDTO> _customer;
        private readonly List<CarDTO> _car;
        private readonly List<EstablishmentDTO> _establishment;
        private readonly List<ReservationDTO> _reservations;

        public CheckReservationsWindow(CarRentalApplication application) {
            InitializeComponent();

            _application = application;

            _establishment = _application.GetEstablishments();
            _car = _application.GetCar();
            _customer = _application.GetCustomers();
            _reservations = _application.GetReservations();

            establishmentList.ItemsSource = _establishment;
            reservationList.ItemsSource = _reservations;
        }
    }
}
