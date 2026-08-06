using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views;

public partial class OpportunitiesPage : ContentPage
{
    private List<VolunteerOpportunity> _allOpportunities = new();

    public OpportunitiesPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _allOpportunities = SampleData.GetOpportunities();
        OpportunitiesCollectionView.ItemsSource = _allOpportunities;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue?.Trim() ?? string.Empty;

        OpportunitiesCollectionView.ItemsSource = string.IsNullOrEmpty(query)
            ? _allOpportunities
            : _allOpportunities
                .Where(o => o.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
    }

    private async void OnOpportunityTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int id)
        {
            await Shell.Current.GoToAsync($"{nameof(OpportunityDetailsPage)}?Id={id}");
        }
    }
}