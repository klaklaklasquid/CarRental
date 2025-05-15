using CarRental.Domain;
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

        public MainWindow(DomainManager domainManager) {
            InitializeComponent();

            _domainManager = domainManager;

            loginListNames.ItemsSource = _domainManager.GetCustomers();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (userInput.Text.Length > 0) {
                placeholder.Opacity = 0;
            } else {
                placeholder.Opacity = 1;
            }
        }
    }
}
