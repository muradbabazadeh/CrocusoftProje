using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CrocusoftProje.Infrastructure.Identity
{
    public interface IClaimsManager
    {
        int GetCurrentUserId();

        string GetCurrentUserName();

        IEnumerable<Claim> GetUserClaims(User user);

        Claim GetUserClaim(string claimType);
    }
}
