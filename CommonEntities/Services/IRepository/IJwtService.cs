using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CommonEntities.Services.IRepository
{
    public interface IJwtService
    {
        string GenerateJSONWebToken(Claim[] claim);
        string ReadJwtToken(string token);
    }

}