using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views;

[QueryProperty(nameof(OpportunityId), "OpportunityId")]
[QueryProperty(nameof(RegistrationId),"RegistrationId")]
public partial class RegistrationPage : ContentPage
{
    private readonly DatabaseService _databaseService;
	private VolunteerRegistration? _existingRegistration;

	public int RegistrationId {get; set; }
    public int OpportunityId { get; set; }

    public RegistrationPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		if(RegistrationId !=0)
		{
			await LoadExistingRegistrationAsync();
		}
		else
		{
			var opportunity = await _databaseService.GetOpportunityByIdAsync(OpportunityId);
			OpportunityTitleLabel.Text = opportunity != null
				? $"Registering for: {opportunity.Title}"
				: "Opportunity not found";
		}

    }

	private async Task LoadExistingRegistrationAsync()
    {
        var registrations = await _databaseService.GetRegistrationsAsync();
        _existingRegistration = registrations.FirstOrDefault(r => r.Id == RegistrationId);

        if (_existingRegistration == null) return;

        // 预填表单
        PreferredNameEntry.Text = _existingRegistration.PreferredName;
        ContactEntry.Text = _existingRegistration.ContactDetail;
        AvailabilityEntry.Text = _existingRegistration.Availability;
        NotesEditor.Text = _existingRegistration.Notes;
        ConsentCheckBox.IsChecked = _existingRegistration.ConsentGiven;

        SubmitButton.Text = "Update Registration";
        DeleteButton.IsVisible = true;              
    }

	private async void OnDeleteClicked(object? sender, EventArgs e)
	{
		if(_existingRegistration == null)return;

		bool confirmed = await DisplayAlertAsync
		(
			"Delete Registration",
			"Are you sure you you want to delete this Registration?",
			"Delete",
			"Cancel"
		);

		if(!confirmed)
			return;

		await _databaseService.DeleteRegistrationsAsync(_existingRegistration);

		await DisplayAlertAsync("Delete", "Registration delete successfully.", "OK");

		await Shell.Current.GoToAsync("..");
	}

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        // Validation — required fields + consent
        if (string.IsNullOrWhiteSpace(PreferredNameEntry.Text) ||
            string.IsNullOrWhiteSpace(ContactEntry.Text) ||
            string.IsNullOrWhiteSpace(AvailabilityEntry.Text))
        {
            ShowError("Please fill in all required fields.");
            return;
        }

        if (!ConsentCheckBox.IsChecked)
        {
            ShowError("You must provide consent before submitting.");
            return;
        }

        var registration = new VolunteerRegistration
        {
			Id = _existingRegistration?.Id ?? 0,
            OpportunityId = _existingRegistration?.OpportunityId?? OpportunityId,
            PreferredName = PreferredNameEntry.Text.Trim(),
            ContactDetail = ContactEntry.Text.Trim(),
            Availability = AvailabilityEntry.Text.Trim(),
            Notes = NotesEditor.Text?.Trim() ?? string.Empty,
            ConsentGiven = true,
            RegistrationDate = _existingRegistration?.RegistrationDate ?? DateTime.Now
        };

        await _databaseService.SaveRegistrationAsync(registration);

        await DisplayAlertAsync("Success", "Your registration has been saved.", "OK");
        await Shell.Current.GoToAsync($"//{nameof(MyRegistrationsPage)}");
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }
}