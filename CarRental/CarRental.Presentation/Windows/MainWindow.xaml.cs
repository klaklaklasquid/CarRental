using CarRental.Domain;
using CarRental.Domain.DTOs;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.Presentation.Windows {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly CarRentalApplication _application;

        private readonly List<CustomerDTO> _customers;
        private IEnumerable<CustomerDTO> _linqQuery;

        public MainWindow(CarRentalApplication application) {
            InitializeComponent();
            _application = application;

            _customers = _application.GetCustomers();
            _linqQuery = _customers;
            loginListNames.ItemsSource = _linqQuery;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (userInput.Text.Length > 0) {
                placeholder.Opacity = 0;
            } else {
                placeholder.Opacity = 1;
            }

            string filter = userInput.Text.Trim().ToLower();

            _linqQuery = string.IsNullOrWhiteSpace(filter)
                ? _customers
                : _customers.Where(c => {
                    var fullName = $"{c.FirstName} {c.LastName}".Trim();
                    return fullName.StartsWith(filter, StringComparison.CurrentCultureIgnoreCase);
                });

            loginListNames.ItemsSource = _linqQuery;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e) {
            if (loginListNames.SelectedItem == null) {
                MessageBox.Show("Please select a customer from the list.");
                return;
            }
            _application.ChangeWindow(this, ((CustomerDTO)loginListNames.SelectedItem));
        }
    }
}
