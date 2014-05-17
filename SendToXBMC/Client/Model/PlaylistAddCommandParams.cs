using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SendToXBMC.Client.Model
{
    [DataContract]
    class PlaylistAddCommandParams: JsonRpcCommandParams
    {
        [DataMember (Name = "playlistid")]
        public int PlaylistId { get; set; }

        [DataMember (Name = "item")]
        public PlaylistItem Item { get; set; }
    }
}
