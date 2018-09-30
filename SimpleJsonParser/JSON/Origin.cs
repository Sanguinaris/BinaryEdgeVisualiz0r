using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJsonParser.JSON
{
    [DataContract]
    class Origin
    {
        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string job_id { get; set; }

        [DataMember]
        public string client_id { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string ts { get; set; }

        [DataMember]
        public string ip { get; set; }
    }
}
