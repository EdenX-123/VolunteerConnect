using ObjCBindings;
using VolunteerConnect.Models;

namespace VolunteerConnect.Views;

public partial class OpportunityDetailsPage : ContentPage
{
	public OpportunityDetailsPage()
	{
		InitializeComponent();
	}

public static List<VolunteerOpportunity> GetSample() => new()
	{
		new VolunteerOpportunity { Id=1, Title="Community Garden Helper", Category="Environment", },
	};
}