using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PowerliftingCapstone.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		public DbSet<ActualProgramTotal> ActualProgramTotals { get; set; }

		public DbSet<UserProfile> UserProfiles { get; set; }

		public DbSet<ExpectedProgramTotal> ExpectedProgramTotals { get; set; }

		public DbSet<Lift> Lifts { get; set; }

		public DbSet<OneRepMax> OneRepMaxes { get; set; }

		public DbSet<SavedWorkout> SavedWorkouts { get; set; }

		public DbSet<SavedWorkoutDateTime> SavedWorkoutDateTimes { get; set; }

		//public DbSet<ApplicationUser> ApplicationUsers { get; set; }

		public DbSet<WeeklyTotal> WeeklyTotals { get; set; }

		public DbSet<WorkoutSerialization> WorkoutSerializations { get; set; }
		public DbSet<Thread> Threads { get; set; }
		public DbSet<Post> Posts { get; set; }
	}
}