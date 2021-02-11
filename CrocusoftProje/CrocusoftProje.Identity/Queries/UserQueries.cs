using AutoMapper;
using CrocusoftProje.Domain.AggregatesModel.RoleAggregate;
using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.Identity.ViewModel;
using CrocusoftProje.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrocusoftProje.Identity.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly CrocusoftProjeDbContext _context;
        private readonly IMapper _mapper;

        public UserQueries(CrocusoftProjeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> FindAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserWithPermissionsAsync(int userId)
        {
            return await _context.Users
           .Include(u => u.Roles)
                .ThenInclude(r => r.Role)
                    .ThenInclude(r => r.Permissions)
                        .ThenInclude(p => p.Permission)
                            .ThenInclude(p => p.Parameters)
            .Include(u => u.Roles)
                .ThenInclude(r => r.Role)
                    .ThenInclude(r => r.Permissions)
                        .ThenInclude(p => p.ParameterValues)
            .Include(u => u.Permissions)
                .ThenInclude(p => p.ParameterValues)
            .Include(u => u.Permissions)
                .ThenInclude(p => p.Permission)
                    .ThenInclude(p => p.Parameters)
                                .Where(u => u.Id == userId)
                                .AsNoTracking()
                                .SingleOrDefaultAsync();
        }

        public async Task<IDictionary<string, PermissionDTO>> GetPermissionsAsync(int userId)
        {
            var user = await GetUserWithPermissionsAsync(userId);

            return GetPermissions(user);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserProfileDTO> GetUserProfileAsync(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (user == null) return null;
            var profile = _mapper.Map<UserProfileDTO>(user);
            profile.Permissions = await GetPermissionsAsync(user.Id);
            return profile;
        }

        private IDictionary<string, PermissionDTO> GetPermissions(User user)
        {
            if (user == null) return null;

            var permissions = user.Permissions;

            var permissionDTOs = _mapper.Map<List<PermissionDTO>>(permissions);
            var rolePermissions = user.Roles.SelectMany(r => r.Role.Permissions.Where(rp => !permissions.Select(p => p.Permission.Id).Contains(rp.Permission.Id)));
            permissionDTOs.AddRange(_mapper.Map<List<PermissionDTO>>(rolePermissions));

            var distinctPermissions = permissionDTOs.GroupBy(p => p.Name).Select(p => p.First());
            return distinctPermissions.ToDictionary(k => k.Name);
        }

        public async Task<IEnumerable<GroupUserDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();

            return _mapper.Map<IEnumerable<GroupUserDTO>>(users);
        }


        public async Task<User> GetUserEntityAsync(int? userId)
        {
            var user = await _context.Users
               .Where(u => u.Id == userId)
               .AsNoTracking()
               .SingleOrDefaultAsync();

            if (user == null) return null;

            return user;
        }

        public async Task<string> GetExistingUser(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user != null)
            {
                return userName;
            }

            return "";
        }

        public async Task<Role> GetRoleAsyncById(int? id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(p => p.Id == id);

            if (role == null) return null;

            return role;
        }
    }
}
