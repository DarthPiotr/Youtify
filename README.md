# Youtify
Copying your playslists between Youtube and Spotify

# Easy to use
```C#
// Init Secrets
YoutifyConfig.YouTubeApiKey   = "Your YouTube API Key";
YoutifyConfig.SpotifyClientId = "Your Spotify Client Id";

// Init services and treat like generic ones
ServiceHandler spotify = new SpotifyHandler();
ServiceHandler youtube = new YouTubeHandler();

// Import and convert playlist
Playlist sourcePlaylist = youtube.ImportPlaylist("PLi9drqWffJ9FWBo7ZVOiaVy0UQQEm4IbP");
ConvertResult convertionResult = Algorithm.Convert(sourcePlaylist.Songs, spotify);

// Prepare new playlist
Playlist newPlaylist = spotify.NewPlaylist(sourcePlaylist); // Get new playlist and copy metadata
newPlaylist.Songs = convertionResult.GetSongs();            // Add songs that were sucessfully converted

// Export playlist
spotify.CreatePlaylist(ref newPlaylist);                    // Create new playlist
spotify.ExportPlaylist(newPlaylist, ExportType.AddAll);     // Export tracks
```
