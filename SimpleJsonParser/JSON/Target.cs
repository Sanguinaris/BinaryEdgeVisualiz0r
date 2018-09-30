using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJsonParser.JSON
{
    [DataContract]
    class Target
    {
        [DataMember]
        public string ip { get; set; }

        [DataMember]
        public int port { get; set; }

        [DataMember]
        public string protocol { get; set; }
    }
}
