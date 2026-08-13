namespace VolunteerConnect.Views;

public partial class PrivacyInfoPage : ContentPage
{
	public PrivacyInfoPage()
	{
		InitializeComponent();
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();

		CloseButton.IsVisible = Navigation.ModalStack.Count > 0;
	}
	private async void OnCloseClicked(object? sender, EventArgs e)
	{
		if (Navigation.ModalStack.Count > 0)
		{
			await Navigation.PopModalAsync();
		}
	}
}