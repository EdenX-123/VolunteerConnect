using VolunteerConnect.Services;
namespace VolunteerConnect.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		LoadFeaturedOpportunity();
    }

	private void LoadFeaturedOpportunity()
	{
		        // Pull the (currently fake) data list from SampleData.cs
        var opportunities = SampleData.GetOpportunities();
 
        // Pick the first one that's available as our "featured" pick.
        // FirstOrDefault returns null if nothing matches, so we check for that.
        var featured = opportunities.FirstOrDefault(o => o.IsAvailable);
 
        if (featured != null)
        {
            // Here's where x:Name pays off — FeaturedImage, FeaturedTitleLabel etc.
            // are the exact names you gave the controls in the XAML.
            FeaturedImage.Source = featured.ImageName;
            FeaturedTitleLabel.Text = featured.Title;
            FeaturedCategoryLabel.Text = featured.Category;
        }
 
        // Count label — built dynamically instead of hardcoded
        OpportunitiesCountLabel.Text = $"{opportunities.Count} opportunities available";

	}
	    // This method name must match the Clicked="..." value in the XAML exactly.
    private async void OnBrowseOpportunitiesClicked(object sender, EventArgs e)
    {
        // Shell.Current.GoToAsync navigates using the route name.
        // nameof(OpportunitiesPage) just gives you the string "OpportunitiesPage" safely
        // (so if you rename the class later, this won't silently break).
        await Shell.Current.GoToAsync(nameof(OpportunitiesPage));
    }

}