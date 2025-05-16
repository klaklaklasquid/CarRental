using CarRental.Domain;
using System.Windows;

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

        public void SetSelectedName(string name) {
            userName.Text = char.ToUpper(name[0]) + name.Substring(1);
        }
    }
}
