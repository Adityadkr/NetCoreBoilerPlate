using System;
using System.Collections.Generic;
using System.Text;

namespace CommonEntities.Enums.Api
{
    public class ApiCommonCode
    {

        public enum API_CODE
        {
            Ok = 200,
            DataNotFound = 404,
            BadRequest = 400,
            UnAuthorizedAccess = 403

        }
        public enum API_STATUS
        {
            Success = 100,
            Failure = 104
        }
       
    }
}
