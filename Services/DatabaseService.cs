using SQLite;
using SQLitePCL;
using VolunteerConnect.Models;

namespace VolunteerConnect.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;

        private const string DatabaseFileName = "volunteerconnect.db3";

        private static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);

        public async Task InitialiseAsync()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(DatabasePath);
            await _database.CreateTableAsync<VolunteerOpportunity>();
            await _database.CreateTableAsync<VolunteerRegistration>();

            await SeedOpportunitiesAsync();
        }

        private async Task SeedOpportunitiesAsync()
        {
            var existingCount = await _database!.Table<VolunteerOpportunity>().CountAsync();
            if (existingCount > 0)
                return;

            var opportunities = new List<VolunteerOpportunity>
            {
                new VolunteerOpportunity
                {
                    Title = "Community Garden Helper",
                    Category = "Environment",
                    Date = DateTime.Now.AddDays(5),
                    Time = "9:00 AM - 12:00 PM",
                    Location = "Auckland Domain",
                    Description = "Help maintain the community garden beds, plant seasonal vegetables, and assist with general upkeep.",
                    Requirements = "No experience needed. Wear closed-toe shoes.",
                    AvailablePlaces = 6,
                    ImageName = "garden.png",
                    IsAvailable = true
                },
                new VolunteerOpportunity
                {
                    Title = "Library Support Volunteer",
                    Category = "Education",
                    Date = DateTime.Now.AddDays(8),
                    Time = "1:00 PM - 4:00 PM",
                    Location = "Central City Library",
                    Description = "Assist with shelving books, helping visitors find resources, and supporting the children's reading corner.",
                    Requirements = "Friendly manner, basic organisation skills.",
                    AvailablePlaces = 3,
                    ImageName = "library.png",
                    IsAvailable = true
                },
                new VolunteerOpportunity
                {
                    Title = "Food Bank Packing Assistant",
                    Category = "Community Support",
                    Date = DateTime.Now.AddDays(2),
                    Time = "10:00 AM - 1:00 PM",
                    Location = "Community Food Bank, Mt Eden",
                    Description = "Sort and pack food donations into hampers for distribution to families in need.",
                    Requirements = "Able to lift light boxes. Closed-toe shoes required.",
                    AvailablePlaces = 0,
                    ImageName = "foodbank.png",
                    IsAvailable = false
                },
                new VolunteerOpportunity
                {
                    Title = "Beach Clean-up Volunteer",
                    Category = "Environment",
                    Date = DateTime.Now.AddDays(12),
                    Time = "8:00 AM - 10:00 AM",
                    Location = "Mission Bay Beach",
                    Description = "Join a group clean-up to remove litter and plastic waste from the shoreline.",
                    Requirements = "Bring gloves if you have them; some provided.",
                    AvailablePlaces = 15,
                    ImageName = "beach.png",
                    IsAvailable = true
                }
            };

            await _database.InsertAllAsync(opportunities);
        }
        public async Task<List<VolunteerOpportunity>> GetOpportunitiesAsync()
        {
            await InitialiseAsync();
            return await _database!.Table<VolunteerOpportunity>().ToListAsync();
        }

        public async Task<VolunteerOpportunity?> GetOpportunityByIdAsync(int id)
        {
            await InitialiseAsync();
            return await _database!.Table<VolunteerOpportunity>()
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<VolunteerRegistration>> GetRegistrationsAsync()
        {
            await InitialiseAsync();
            return await _database!.Table<VolunteerRegistration>().ToListAsync();
        }


        public async Task<int> SaveRegistrationAsync(VolunteerRegistration registration)
        {
            await InitialiseAsync();

            if(registration.Id !=0)
                return await _database!.UpdateAsync(registration);

            return await _database!.InsertAsync(registration);
        }


        public async Task<int> DeleteRegistrationsAsync(VolunteerRegistration registration)
        {
            await InitialiseAsync();
            return await _database!.DeleteAsync(registration);
        }


    }
}