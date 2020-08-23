# Youtify
Copying your playslists between Youtube and Spotify

# Easy to use
```C#
var spotify = new SpotifyHandler(); // creates a Spotify instance
var youtube = new YouTubeHandler(); // creates a YouTube instance

// converts playlist from YouTube, with given Id, to Spotify playlist
var resp = Algorithm.Convert(youtube, "PLi9drqWffJ9FWBo7ZVOiaVy0UQQEm4IbP", spotify);
```
