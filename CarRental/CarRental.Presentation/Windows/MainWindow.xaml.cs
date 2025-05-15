using CarRental.Domain;
using CarRental.Domain.DTOs;
using CarRental.Domain.Model;
using CarRental.Domain.Sevices;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly DomainManager _domainManager;
        private readonly CarRentalApplication _application;

        private readonly List<CustomerDTO> _customers;
        private IEnumerable<CustomerDTO> _linqQuery;

        public MainWindow(CarRentalApplication application, DomainManager domainManager) {
            InitializeComponent();
            _domainManager = domainManager;
            _application = application;

            _customers = _domainManager.GetCustomers();
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
            _application.ChangeWindow(this);
        }
    }
}
