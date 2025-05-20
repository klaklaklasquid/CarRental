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
        private readonly List<ReservationDTO> _allReservations;
        private CustomerDTO _user;

        public CheckReservationsWindow(CarRentalApplication application) {
            InitializeComponent();

            _application = application;

            _allReservations = _application.GetReservations();
            _establishment = _application.GetEstablishments();
            establishmentList.ItemsSource = _establishment;
            reservationList.ItemsSource = _allReservations;

            searchName.TextChanged += FilterChanged;
            searchDate.SelectedDateChanged += FilterChanged;
            establishmentList.SelectionChanged += FilterChanged;

            this.Activated += (s, e) => LoadReservations();
        }

        public void GetCustomer(CustomerDTO user) {
            _user = user;
        }

        private void FilterChanged(object sender, EventArgs e) {
            string name = searchName.Text.Trim().ToLower();
            DateTime? date = searchDate.SelectedDate;
            var selectedEstablishment = establishmentList.SelectedItem as EstablishmentDTO;

            var filtered = _allReservations.Where(r =>
                (string.IsNullOrEmpty(name) ||
                    (r.Customer.FirstName + " " + r.Customer.LastName).ToLower().Contains(name)) &&
                (!date.HasValue ||
                    (r.StartDate.Date <= date.Value.Date && r.EndDate.Date >= date.Value.Date)) &&
                (selectedEstablishment == null ||
                    r.Establishment.Id == selectedEstablishment.Id)
            ).ToList();

            reservationList.ItemsSource = filtered;
        }

        private void establishmentList_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            var item = ItemsControl.ContainerFromElement(establishmentList, e.OriginalSource as DependencyObject) as ListViewItem;
            if (item != null && item.IsSelected) {
                establishmentList.SelectedItem = null;
                e.Handled = true;
            }
        }

        private void LoadReservations() {
            _allReservations.Clear();
            _allReservations.AddRange(_application.GetReservations());
            FilterChanged(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            ReservationDTO selectedReservation = (ReservationDTO)reservationList.SelectedItem;
            if (selectedReservation == null) {
                MessageBox.Show("Please select a reservation to cancel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (selectedReservation.Customer.Email == _user.Email) {
                _application.DeleteReservation(selectedReservation);
                LoadReservations();
                MessageBox.Show("Reservation cancelled successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else {
                MessageBox.Show("Error: You can only cancel your own reservation.", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
