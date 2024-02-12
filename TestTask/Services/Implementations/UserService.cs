using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User> GetUser()
        {
            var user = _context.Users
                .AsNoTracking()
                .Where(u => u.Orders.Any())
                .OrderByDescending(u => u.Orders.Count)
                .FirstOrDefaultAsync();

            return user;
        }

        public Task<List<User>> GetUsers()
        {
            var users = _context.Users
                 .AsNoTracking()
                 .Where(u => u.Status == Enums.UserStatus.Inactive)
                 .ToListAsync();

            return users;
        }
    }
}
