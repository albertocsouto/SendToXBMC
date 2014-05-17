﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace YoutubeToXBMC.Client.Model
{
    [DataContract]
    class PlaylistClearCommandParams: JsonRpcCommandParams
    {
        
        [DataMember (Name="playlistid")]
        public int PlaylistId { get; set; }
    }
}
