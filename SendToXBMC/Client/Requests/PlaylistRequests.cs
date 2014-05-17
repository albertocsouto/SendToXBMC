using System;
using System.Threading.Tasks;
using Windows.Data.Json;
using SendToXBMC.Client.Model;
using SendToXBMC.Util;

namespace SendToXBMC.Client.Requests
{
    class PlaylistRequests
    {
        public static async Task<Boolean> playVideo(String URL)
        {
            String videoId = VideoURLParser.youtubeVideoIdFromUrl(URL);
            if (!videoId.Equals(String.Empty))
            {
                PlaylistClearCommandParams clearCommandParams = new PlaylistClearCommandParams();
                clearCommandParams.PlaylistId = 1;
                JsonRpcCommand clearCommand = new JsonRpcCommand("1", "Playlist.Clear", clearCommandParams);
                JsonObject clearResult = await XBMCClient.sendRequestWithCommand(clearCommand);
                if (clearResult != null)
                {
                    PlaylistItem playlistItem = new PlaylistItem();
                    playlistItem.File = "plugin://plugin.video.youtube/?action=play_video&videoid=" + videoId;
                    PlaylistAddCommandParams addParams = new PlaylistAddCommandParams();
                    addParams.PlaylistId = 1;
                    addParams.Item = playlistItem;
                    JsonRpcCommand addCommand = new JsonRpcCommand("1", "Playlist.Add", addParams);
                    JsonObject addResult = await XBMCClient.sendRequestWithCommand(addCommand);
                    if (addResult != null)
                    {
                        PlayerItem playerItem = new PlayerItem();
                        playerItem.PlaylistId = 1;
                        PlayerOpenCommandParams playerOpenParams = new PlayerOpenCommandParams();
                        playerOpenParams.Item = playerItem;
                        JsonRpcCommand playerOpenCommand = new JsonRpcCommand("1", "Player.Open", playerOpenParams);
                        JsonObject playResult = await XBMCClient.sendRequestWithCommand(playerOpenCommand);
                        if (playResult != null)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
