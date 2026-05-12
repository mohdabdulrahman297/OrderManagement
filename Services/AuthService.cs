using AuthService.Data;
using AuthService.Entities;
using AuthService.Helpers;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class AuthService
    {
        private readonly AuthDbContext _context;
        private readonly TokenService _tokenService;

        public AuthService(AuthDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // ── REGISTER CUSTOMER ──────────────────────────────────────────
        public async Task<bool> RegisterCustomerAsync(CustomerRegisterModel model)
        {
            // Check if username or email already exists
            bool exists = await _context.Users
                .AnyAsync(u => u.Username == model.Username || u.EmailId == model.EmailId);

            if (exists) return false;

            var salt = PasswordHelper.GenerateSalt();
            var hashedPassword = PasswordHelper.HashPassword(model.Password, salt);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                EmailId = model.EmailId,
                Password = hashedPassword,
                Salt = salt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Assign Customer role (RoleId = 2)
            _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = 2 });
            await _context.SaveChangesAsync();

            return true;
        }

        // ── REGISTER ADMIN ─────────────────────────────────────────────
        public async Task<bool> RegisterAdminAsync(CustomerRegisterModel model)
        {
            bool exists = await _context.Users
                .AnyAsync(u => u.Username == model.Username || u.EmailId == model.EmailId);

            if (exists) return false;

            var salt = PasswordHelper.GenerateSalt();
            var hashedPassword = PasswordHelper.HashPassword(model.Password, salt);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                EmailId = model.EmailId,
                Password = hashedPassword,
                Salt = salt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Assign Admin role (RoleId = 1)
            _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = 1 });
            await _context.SaveChangesAsync();

            return true;
        }

        // ── LOGIN ──────────────────────────────────────────────────────
        public async Task<UserLoginResponseViewModel?> LoginAsync(LoginModel model)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null) return null;

            // Verify the entered password against stored hash
            bool isValid = PasswordHelper.VerifyPassword(model.Password, user.Password, user.Salt);
            if (!isValid) return null;

            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
            var token = _tokenService.GenerateToken(user, roles);

            return new UserLoginResponseViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                EmailId = user.EmailId,
                Token = token,
                Roles = roles
            };
        }

        // ── UPDATE PROFILE ─────────────────────────────────────────────
        public async Task<bool> UpdateUserAsync(int userId, UpdateModel model)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.EmailId = model.EmailId;

            await _context.SaveChangesAsync();
            return true;
        }

        // ── DELETE ACCOUNT ─────────────────────────────────────────────
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return false;

            _context.UserRoles.RemoveRange(user.UserRoles);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // ── GET SINGLE USER ────────────────────────────────────────────
        public async Task<UserLoginResponseViewModel?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            return new UserLoginResponseViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                EmailId = user.EmailId,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };
        }

        // ── GET ALL USERS (paginated) ──────────────────────────────────
        public async Task<List<UserLoginResponseViewModel>> GetAllUsersAsync(PaginationFilter filter)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(u => new UserLoginResponseViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username,
                    EmailId = u.EmailId,
                    Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList()
                })
                .ToListAsync();
        }
    }
}