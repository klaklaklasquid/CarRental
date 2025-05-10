using Microsoft.Win32;
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

namespace CarRental.DataInjectionPresentation.Windows {
    /// <summary>
    /// Interaction logic for InjectDataWindow.xaml
    /// </summary>
    public partial class InjectDataWindow : Window {
        public InjectDataWindow() {
            InitializeComponent();
        }

        private string SelectPathName() {
            OpenFileDialog fileDialog = new();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSV files (*.csv)|*.csv";

            return fileDialog.ShowDialog() == true ? fileDialog.FileName : string.Empty;
        }

        private void Click_Select_Establishment(object sender, RoutedEventArgs e) {
            string fileName = SelectPathName();
            FilePathEstablishment.Text = fileName;
        }

        private void Click_Select_Car(object sender, RoutedEventArgs e) {
            string fileName = SelectPathName();
            FilePathCar.Text = fileName;
        }

        private void Click_Select_Customer(object sender, RoutedEventArgs e) {
            string fileName = SelectPathName();
            FilePathCustomer.Text = fileName;
        }


        private void FilledIn(object sender, TextChangedEventArgs e) {
            Send_Data.IsEnabled =
                !string.IsNullOrWhiteSpace(FilePathEstablishment.Text) &&
                !string.IsNullOrWhiteSpace(FilePathCar.Text) &&
                !string.IsNullOrWhiteSpace(FilePathCustomer.Text);
        }

        private void Click_Send_Data(object sender, RoutedEventArgs e) {

        }
    }
}
