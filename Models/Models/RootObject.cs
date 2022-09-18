using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities
{
    public class RootObject<T>
    {
        
            public string OPERATION { get; set; }
            public string SENDERID { get; set; }
            public int RESPONSECODE { get; set; }
            public string RESPONSEMESSAGE { get; set; }
            public string RESPONSEMESSAGEDETAIL { get; set; }
            public List<T> VALUES { get; set; } = new List<T>();

        
    }
}
