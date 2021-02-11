using CrocusoftProje.Domain.AggregatesModel.RoleAggregate;
using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.Identity.ViewModel;
using CrocusoftProje.SharedKernel.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrocusoftProje.Identity.Queries
{
    public interface IUserQueries : IQuery
    {
        Task<IDictionary<string, PermissionDTO>> GetPermissionsAsync(int userId);

        Task<User> GetUserWithPermissionsAsync(int userId);

        Task<User> FindByNameAsync(string userName);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindAsync(int userId);

        Task<UserProfileDTO> GetUserProfileAsync(int userId);

        Task<User> GetUserEntityAsync(int? userId);

        Task<IEnumerable<GroupUserDTO>> GetAllUsersAsync();

        Task<string> GetExistingUser(string userName);

        Task<Role> GetRoleAsyncById(int? id);
    }
}
