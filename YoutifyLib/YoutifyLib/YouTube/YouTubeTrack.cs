using Google.Apis.YouTube.v3.Data;

namespace YoutifyLib.YouTube
{
    /// <summary>
    /// An instance of YouTube Track
    /// </summary>
    public class YouTubeTrack : Track
    {
        public string PlaylistItemId { get; set; }

        public YouTubeTrack() : base() { }

        public YouTubeTrack(Video vid) : base()
        {
            Metadata = Algorithm.Algorithm.GetMetadata(vid.Snippet.Title);
            ID = vid.Id;
        }
        public YouTubeTrack(PlaylistItem pli) : base()
        {
            Metadata = Algorithm.Algorithm.GetMetadata(pli.Snippet.Title);
            ID = pli.Snippet.ResourceId.VideoId;
            PlaylistItemId = pli.Id;
        }
        
        /// <summary>
        /// Creates YouTube API's PlaylistItem instance from Track
        /// </summary>
        /// <param name="playlistId">Id of a playlist that track will be part of</param>
        /// <returns>instance of YT API's PlaylistItem</returns>
        public Google.Apis.YouTube.v3.Data.PlaylistItem ToPlaylistItem(string playlistId)
        {
            return new PlaylistItem {
                Snippet = new PlaylistItemSnippet
                {
                    ResourceId = new ResourceId
                    {
                        Kind = "youtube#video",
                        VideoId = ID,
                        PlaylistId = playlistId
                    }
                }
            };
        }
    }
}
