using CarRental.Domain;
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
    /// Interaction logic for ChooseOptionWindow.xaml
    /// </summary>
    public partial class ChooseOptionWindow : Window {
        private readonly DomainManager _domainManager;

        public ChooseOptionWindow(DomainManager domainManager) {
            InitializeComponent();

            _domainManager = domainManager;
        }
    }
}
