using System;
using System.Collections.Generic;
using System.Text;

namespace CommonEntities.Services.IRepository
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        bool RemoveData(string key);

    }
}
