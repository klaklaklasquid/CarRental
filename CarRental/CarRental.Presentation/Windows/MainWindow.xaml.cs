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

        public MainWindow(CarRentalApplication application) {
            InitializeComponent();
            _application = application;
            loginListNames.ItemsSource = _application.GetFilterUserMainScreen(userInput.Text.Trim().ToLower());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (userInput.Text.Length > 0) {
                placeholder.Opacity = 0;
            } else {
                placeholder.Opacity = 1;
            }

            loginListNames.ItemsSource = _application.GetFilterUserMainScreen(userInput.Text.Trim().ToLower());
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
