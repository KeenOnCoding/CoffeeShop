﻿using System.Security.Claims;

namespace CoffeeShop.Core.AuthContext
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(string userId, string email, IEnumerable<Claim> additionalClaims);
    }
}