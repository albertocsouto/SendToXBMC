using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace YoutubeToXBMC.Util
{
    class VideoURLParser
    {
        public static readonly Regex YoutubeVideoRegex = new Regex(@"(/[^watch]|=)([a-zA-Z0-9-]|_)+($|(\?|&))", RegexOptions.IgnoreCase);
        public static readonly Regex VideoRegex = new Regex(@"([a-zA-Z0-9-]|_)+");
        
        public static String youtubeVideoIdFromUrl(String URL)
        {
            String id = String.Empty;
            Match youtubeMatch = YoutubeVideoRegex.Match(URL);
            if (youtubeMatch.Success)
            {
                string videoURL = youtubeMatch.Groups[0].Value;
                Match idMatch = VideoRegex.Match(videoURL);
                id = idMatch.Groups[0].Value;
            }
            return id;
        }
    }
}
