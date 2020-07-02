using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFContract
{
    [DataContract]
    [KnownType(typeof(Dictionary<string, string>))]
    public struct MSG
    {
        [DataMember]
        public bool statusOp;
        [DataMember]
        public string info;
        [DataMember]
        public string operationName;
        [DataMember]
        public string tokenApp;
        [DataMember]
        public string tokenUser;
        [DataMember]
        public string appVersion;
        [DataMember]
        public string operationVersion;
        [DataMember]
        public object[] data;
    }
}
