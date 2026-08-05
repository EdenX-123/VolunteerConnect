using VolunteerConnect.Models;

namespace VolunteerConnect.Services;

// TEMPORARY hardcoded data for Day 4 UI work.
// Replace with SQLite reads once VolunteerDatabaseService is built (Week 4).
public static class SampleData
{
    public static List<VolunteerOpportunity> GetOpportunities() => new()
    {
        new VolunteerOpportunity
        {
            Id = 1,
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
            Id = 2,
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
            Id = 3,
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
            Id = 4,
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

    public static VolunteerOpportunity? GetById(int id) =>
        GetOpportunities().FirstOrDefault(o => o.Id == id);
}
