using VolunteerConnect.Services;

namespace VolunteerConnect.Views;

public partial class MyRegistrationsPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public MyRegistrationsPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

	private async void OnRegistrationTapped(object? sender, TappedEventArgs e)
	{
        if (e.Parameter is int id)
		{
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}?RegistrationId={id}");
		}
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        RegistrationsCollectionView.ItemsSource = await _databaseService.GetRegistrationsWithOpportunityTitlesAsync();
    }
}