using Microsoft.Extensions.Logging;
using VolunteerConnect.Services;
using VolunteerConnect.Views;
using VolunteerConnect;

namespace VolunteerConnect;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<DatabaseService>();

		builder.Services.AddSingleton<HomePage>();
		builder.Services.AddSingleton<MyRegistrationsPage>();
		builder.Services.AddSingleton<PrivacyInfoPage>();

		builder.Services.AddTransient<OpportunitiesPage>();
		builder.Services.AddTransient<OpportunityDetailsPage>();
		builder.Services.AddTransient<RegistrationPage>();


		return builder.Build();
	}
}
