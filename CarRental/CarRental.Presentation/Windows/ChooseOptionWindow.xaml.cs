using CarRental.Domain;
using CarRental.Domain.DTOs;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.Presentation.Windows {
    /// <summary>
    /// Interaction logic for ChooseOptionWindow.xaml
    /// </summary>
    public partial class ChooseOptionWindow : Window {
        internal event EventHandler<string> OpenWindowRequested;

        private readonly DomainManager _domainManager;
        private readonly CarRentalApplication _application;

        public ChooseOptionWindow(CarRentalApplication application ,DomainManager domainManager) {
            InitializeComponent();

            _domainManager = domainManager;
            _application = application;
        }

        public void GetCustomer(CustomerDTO user) {
            string name = user.FirstName;
            userName.Text = char.ToUpper(name[0]) + name.Substring(1);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if(sender is Button button) {
                OpenWindowRequested.Invoke(this, button.Tag.ToString());
            }
        }
    }
}
