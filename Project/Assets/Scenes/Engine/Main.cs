using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using Id3;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Main : MonoBehaviour {
	Playlist currentPlaylist;
	PlaylistItem currentSong;
	bool playlistPlay = true;
	bool shuffle = false;
	int count = 0;
	List<Playlist> allPlaylists = new List<Playlist>();
	AudioSource player;
	AudioClip clp;
	string view = "Playlists";
	float viewLerp = 0;
	Songpool songpool = new Songpool();

	// Use this for initialization
	void Start () {
		//cube.AddComponent (MeshFilter);
		//cube.AddComponent (MeshCollider);
				//player = GetComponent<AudioSource> ();		
		loadAllPlaylists ();	
				songpool.load();
        songpool.sortArtists();
        songpool.show();
		



	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp("a")){
			prev ();
			Debug.Log ("Previous");
		}
		if(Input.GetKeyUp("s")){
			play ();
			Debug.Log ("Play");
		}
		if(Input.GetKeyUp("d")){
			pause ();
			Debug.Log ("Pause");
		}
		if(Input.GetKeyUp("f")){
		next ();
			Debug.Log ("Next");
		}
		if(Input.GetKeyUp("x")){
			shuffleTogle ();
			Debug.Log ("Shuffle: "+shuffle);
		}
		if(Input.GetKeyUp("c")){
			//searchForFiles ("C:/Users/M/Downloads/2003 - Andante");
			//searchForFiles("C:/Users/M/Downloads/Radiohead - The Best Of Radiohead (2008) 320 vtwin88cube");
			//searchForFiles("C:/Users/M/Downloads/Green Day - Greatest Hits (2CD)  2010");
			//searchForFiles("C:/Users/M/Downloads/TAYLOR SWIFT - DISCOGRAPHY (2006-14) [CHANNEL NEO]");
			view = "Songpool";
			viewLerp = 0;
			foreach(PlaylistItem p in songpool.songpool){
				Debug.Log(p.toString());
			}
		}
		setView(view);
		viewLerp += 0.1f * Time.deltaTime;
	}

	void setView(string tmp){
		switch(tmp){
		case "Playlists":
			break;
		case "Songpool":
			GameObject cam = GameObject.Find("MainCAM");
			Vector3 vectA = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
			Vector3 vectB = new Vector3(cam.transform.position.x, cam.transform.position.y+1, cam.transform.position.z);
				cam.transform.position = Vector3.Lerp(vectA,vectB,viewLerp);;
			break;
		}
	}

	void play(){
		player.Play();
	}

	void pause(){
		player.Pause ();

	}

	void next(){
		if(playlistPlay && !shuffle){

				currentSong = currentPlaylist.getNext(count);
				Debug.Log (currentSong.toString());
			count++;
		}

	}

	void prev(){


	}

	void shuffleTogle(){
		shuffle = !shuffle;

	}



	void loadAllPlaylists(){


	}

	void savePlaylist(Playlist pl){

	}

	void newPlaylist(string newName){
		Playlist newPl = new Playlist (newName);
		allPlaylists.Add (newPl);
		currentPlaylist = newPl;

	}

	void deletePlaylist(Playlist PL){
		allPlaylists.Remove (PL);

	}



}
