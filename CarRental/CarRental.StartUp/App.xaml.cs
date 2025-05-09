using CarRental.Domain;
using CarRental.Domain.Repository;
using CarRental.Persistence.Mapper;
using CarRental.Presentation;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CarRental.StartUp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    private void Application_Startup(object sender, StartupEventArgs e) {
        // persistence layer
        ICarRepository carRepository = new CarMapper();
        ICustomerRepository customerRepository = new CustomerMapper();
        IEstablishmentRepository establishmentRepository = new EstablishmentMapper();

        // domain layer
        DomainManager domainManager = new(carRepository, customerRepository, establishmentRepository);

        // presentation layer
        _ = new CarRentalApplication(domainManager);
    }
}

