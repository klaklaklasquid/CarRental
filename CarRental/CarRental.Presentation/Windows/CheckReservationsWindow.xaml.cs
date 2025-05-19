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
        private readonly List<EstablishmentDTO> _establishment;

        public CheckReservationsWindow(CarRentalApplication application) {
            InitializeComponent();

            _application = application;

            _establishment = _application.GetEstablishments();

            establishmentList.ItemsSource = _establishment;

            reservationList.ItemsSource = _application.GetReservations();
        }
    }
}
