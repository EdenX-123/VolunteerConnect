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

        DetailPlaces.Text = _opportunity.IsAvailable
            ?$"{_opportunity.AvailablePlaces} places available"
            :"NO places currently available";

        if(_opportunity.IsAvailable && _opportunity.AvailablePlaces > 0)
        {
            RegisterButton.Background = (Color)Application.Current!.Resources["Primary"];
            RegisterButton.TextColor = Colors.White;
            RegisterButton.Text = "Register Interest";
        }
        else
        {
            RegisterButton.Background = Colors.LightGray;
            RegisterButton.TextColor = Colors.DarkGray;
            RegisterButton.Text = "Fully Booked";
        }
    }

    private async void OnRegisterClicked(object? sender, EventArgs e)
    {
        if (_opportunity == null) return;

        if(!_opportunity.IsAvailable || _opportunity.AvailablePlaces <= 0)
        {
            await DisplayAlertAsync(
                "NO Places Available",
                "This opportunity is currently full. Please check back later or choose another opportunity.",
                "OK"
            );
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}?OpportunityId={_opportunity.Id}");
    }
}