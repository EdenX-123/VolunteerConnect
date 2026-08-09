using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views;

[QueryProperty(nameof(OpportunityId), "Id")]
public partial class OpportunityDetailsPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    private VolunteerOpportunity? _opportunity;

    public int OpportunityId { get; set; }

    public OpportunityDetailsPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadOpportunityAsync();
    }

    private async Task LoadOpportunityAsync()
    {
        _opportunity = await _databaseService.GetOpportunityByIdAsync(OpportunityId);

        if (_opportunity == null)
        {
            // Error handling requirement: missing/invalid record
            DetailTitle.Text = "Opportunity not found";
            DetailDescription.Text = "The selected opportunity could not be found.";
            RegisterButton.IsEnabled = false;
            return;
        }

        DetailImage.Source = _opportunity.ImageName;
        DetailTitle.Text = _opportunity.Title;
        DetailCategory.Text = _opportunity.Category;
        DetailDateTime.Text = $"{_opportunity.Date:dd MMM yyyy} • {_opportunity.Time}";
        DetailLocation.Text = _opportunity.Location;
        DetailDescription.Text = _opportunity.Description;
        DetailRequirements.Text = _opportunity.Requirements;
        DetailPlaces.Text = _opportunity.IsAvailable
            ? $"{_opportunity.AvailablePlaces} places available"
            : "No places currently available";

        RegisterButton.IsEnabled = _opportunity.IsAvailable;
    }

    private async void OnRegisterClicked(object? sender, EventArgs e)
    {
        if (_opportunity == null) return;

        // RegistrationPage doesn't exist yet with real logic (that's Week 4) —
        // for now just navigate through so you can confirm the flow works.
        await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}?OpportunityId={_opportunity.Id}");
    }
}