using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.Identity.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrocusoftProje.Identity.Auth
{
    public interface IUserManager
    {
        int GetCurrentUserId();

        string GetCurrentUserName();

        Task<User> GetCurrentUser();

        (string token, DateTime expiresAt) GenerateJwtToken(User user);

        Task<PermissionDTO> GetPermissionAsync(string permissionName);
    }
}
