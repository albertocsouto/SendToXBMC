using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Runtime.Serialization.Json;
using Windows.UI.Popups;
using Windows.Data.Json;
using SendToXBMC.Util;
using SendToXBMC.Client.Model;


namespace SendToXBMC.Client
{
    class XBMCClient
    {
        public static async Task<JsonObject> sendRequestWithCommand(JsonRpcCommand command)
        {            
            String basePath = "http://" + SettingsManager.XBMCHost() + ":" + SettingsManager.XBMCPort() + "/jsonrpc";

            String body = Serialize(command); 
         
            HttpClient client = new HttpClient();
            if (SettingsManager.XBMCUser() != null && !SettingsManager.XBMCUser().Equals(String.Empty))
            {
                client.DefaultRequestHeaders.Authorization = authorizationHeader();
            }

            try
            {
                HttpResponseMessage response = await client.PostAsync(basePath, new StreamContent(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(body))));
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => JsonObject.Parse(content));
            } catch (Exception e)
            {
                return null;
            }
        }

        private static AuthenticationHeaderValue authorizationHeader()
        {
            return new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.UTF8Encoding.UTF8.GetBytes(
                        String.Format("{0}:{1}", SettingsManager.XBMCUser(), SettingsManager.XBMCPassword()))));
        }

        private static T Deserialize<T>(string json)
        {
            var _Bytes = Encoding.Unicode.GetBytes(json);
            using (MemoryStream _Stream = new MemoryStream(_Bytes))
            {
                var _Serializer = new DataContractJsonSerializer(typeof(T));
                return (T)_Serializer.ReadObject(_Stream);
            }
        }

        private static string Serialize(object instance)
        {
            using (MemoryStream _Stream = new MemoryStream())
            {
                var _Test = new DataContractJsonSerializerSettings();
                _Test.EmitTypeInformation = System.Runtime.Serialization.EmitTypeInformation.Never;
                var _Serializer = new DataContractJsonSerializer(instance.GetType(), _Test);
                _Serializer.WriteObject(_Stream, instance);
                _Stream.Position = 0;
                using (StreamReader _Reader = new StreamReader(_Stream))
                { return _Reader.ReadToEnd(); }
            }
        }
    }
}
