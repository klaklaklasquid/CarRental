using CarRental.DataInjectionPresentation;
using CarRental.Domain;
using CarRental.Domain.Repository;
using CarRental.Persistence.Mapper;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CarRental.DataInjectionStartUp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    private void Application_Startup(object sender, StartupEventArgs e) {
        // persistence layer
        ICarRepository carRepository = new CarMapper();
        ICustomerRepository customerRepository = new CustomerMapper();
        IEstablishmentRepository establishmentRepository = new EstablishmentMapper();
        IReservationRepository reservationRepository = new ReservationMapper();

        // domain layer
        DomainManager domainManager = new DomainManager(
            carRepository,
            customerRepository,
            establishmentRepository,
            reservationRepository
        );

        // presentation layer
        _ = new CarRentalDataInjectionApplication(domainManager);
    }
}

