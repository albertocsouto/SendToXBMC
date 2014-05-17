using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SendToXBMC.Client.Model
{
    [DataContract]
    class PlaylistItem
    {
        [DataMember (Name="file")]
        public string File { get; set; }
    }
}
