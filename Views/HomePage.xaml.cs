using VolunteerConnect.Models;
using VolunteerConnect.Services;
namespace VolunteerConnect.Views;

public partial class HomePage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private VolunteerOpportunity? _featuredOpportunity;
    public HomePage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadFeaturedOpportunityAsync();
    }

    private async Task LoadFeaturedOpportunityAsync()
    {
        // Pull the (currently fake) data list from SampleData.cs
        var opportunities = await _databaseService.GetOpportunitiesAsync();
        var availableOpporturnities = opportunities.Where(o => o.IsAvailable).ToList();

        if (availableOpporturnities.Count > 0)
        {
            var random = new Random();
            var featured = availableOpporturnities[random.Next(availableOpporturnities.Count)];

            _featuredOpportunity = featured;

            FeaturedImage.Source = featured.ImageName;
            FeaturedTitleLabel.Text = featured.Title;
            FeaturedCategoryLabel.Text = featured.Category;

            FeaturedLocationLabel.Text = featured.Location;
            FeaturedDateLabel.Text = featured.Date.ToString("dd MMM yyyy");
            FeaturedTimeLabel.Text = featured.Time;
            FeaturedDescriptionLabel.Text = featured.Description;
            FeaturedPlacesLabel.Text = $"{featured.AvailablePlaces} places available";


        }

        // Count label — built dynamically instead of hardcoded
        OpportunitiesCountLabel.Text = $"{opportunities.Count} opportunities available";

    }

    private async void OnFeaturedCardTapped(object? sender, TappedEventArgs e)
    {
        if (_featuredOpportunity == null) return;

        //testing carshing
        // await Shell.Current.GoToAsync($"{nameof(OpportunityDetailsPage)}?Id=999");

        await Shell.Current.GoToAsync($"{nameof(OpportunityDetailsPage)}?Id={_featuredOpportunity.Id}");

    }
    // This method name must match the Clicked="..." value in the XAML exactly.
    private async void OnBrowseOpportunitiesClicked(object? sender, EventArgs e)
    {
        // Shell.Current.GoToAsync navigates using the route name.
        // nameof(OpportunitiesPage) just gives you the string "OpportunitiesPage" safely
        // (so if you rename the class later, this won't silently break).
        await Shell.Current.GoToAsync($"//{nameof(OpportunitiesPage)}");
    }

}