using SendToXBMC.Client.Model;
using System;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace SendToXBMC.Client.Requests
{
    class JSONRPCRequests
    {
        public static async Task<Boolean> Ping()
        {
            JsonRpcCommand pingCommand = new JsonRpcCommand("1", "JSONRPC.Ping", null);
            JsonObject pingResult = await XBMCClient.sendRequestWithCommand(pingCommand);
            return (pingResult != null);
        }
    }
}
