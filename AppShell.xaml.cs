using VolunteerConnect.Views;
namespace VolunteerConnect;

public partial class AppShell : Shell
{
	public AppShell()
	{

		InitializeComponent();
		Routing.RegisterRoute(nameof(OpportunitiesPage), typeof(OpportunitiesPage));
		Routing.RegisterRoute(nameof(OpportunityDetailsPage), typeof(OpportunityDetailsPage));
		Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
	}


}
