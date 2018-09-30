using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SimpleJsonParser.JSON;

namespace SimpleJsonParser
{
    [DataContract]
    class Json
    {
        [DataMember]
        public Origin origin {get;set;}

        [DataMember]
        public Target target { get; set; }
    }
}
