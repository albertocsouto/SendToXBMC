using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SendToXBMC.Client.Model
{
    [DataContract]
    [KnownType(typeof(PlaylistClearCommandParams))]
    class JsonRpcCommand
    {
        public JsonRpcCommand(string Id, string Method, JsonRpcCommandParams Params)
        {
            this.Id = "1";
            this.JsonRpcVersion = "2.0";
            this.Method = Method;
            this.Params = Params;
        }

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember (Name="jsonrpc")]
        public string JsonRpcVersion { get; set; }
        [DataMember (Name="method")]
        public string Method { get; set; }
        [DataMember (Name="params")]
        public JsonRpcCommandParams Params { get; set; }
    }
}
