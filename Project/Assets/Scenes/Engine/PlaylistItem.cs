using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable()]
public class PlaylistItem : System.Object {
	public string location;
	public string genre;
	public string artist;
	public string album;
	public string title;

	public PlaylistItem(string location, string genre, string artist, string album, string title){
		this.location = location;
		this.album = album;
		this.artist = artist;
		this.genre = genre;
		this.title = title;

	}

	public PlaylistItem(string location){
		this.location = location;
		this.album = "Unknown album";
		this.artist = "Unknown artist";
		this.genre = "Unknown genre";
		this.title = "Unknown title";

	}

	public string toString(){
		return(title + " by " + artist + " from " + album + ". Genre: " + genre);

	}
}
