using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using Id3;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Songpool : System.Object{
	public List<PlaylistItem> songpool = new List<PlaylistItem>();
	IFormatter formatter = new BinaryFormatter();
	GameObject cube;
	GameObject smpltxt;
	List<GameObject> labels = new List<GameObject>();
	MonoBehaviour m = new MonoBehaviour();
	List<Artist> artists = new List<Artist>();

	// Use this for initialization
	void Start () {
		load ();
		sortArtists ();
		show ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void show(){
		cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		float i = 0;
		foreach(PlaylistItem p in songpool){
			
			MonoBehaviour.Instantiate(cube);
			i+=0.1f;
			cube.transform.position = new Vector3(cube.transform.position.x+i, cube.transform.position.y, cube.transform.position.z);
			cube.name = "cube"+i;
			GameObject label = new GameObject(cube.name+" label");
			TextMesh tml = (TextMesh)label.AddComponent<TextMesh>();
			MeshRenderer mrl = (MeshRenderer)label.AddComponent<MeshRenderer>();
			tml.text = cube.name;
			tml.transform.position = cube.transform.position;
			tml.offsetZ = 0;
			tml.characterSize = 0.1f;
			tml.lineSpacing = 1;
			tml.anchor = TextAnchor.UpperLeft;
			tml.alignment = TextAlignment.Center;
			tml.font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
			tml.color = new Color(1,1,1);
			MeshRenderer rend = label.GetComponentInChildren<MeshRenderer>();
			rend.material = tml.font.material;
			
			labels.Add(label);
		}
	}
	
	public void hide(){
	}

	public void searchForFiles(string baseDirectory){
		DirectoryInfo dir = new DirectoryInfo(baseDirectory);
		DirectoryInfo[] directories = dir.GetDirectories();
		foreach (DirectoryInfo d in directories) {
			searchForFiles (d.FullName);		
			
		}
		
		string fn = dir.FullName;
		string[] musicFiles = Directory.GetFiles (@fn,"*.mp3");
		foreach (string musicFile in musicFiles) {
			//using (var mp3 = new Mp3File(musicFile)) {
				Mp3ID3 tag = new Mp3ID3(musicFile);
				//Id3Tag tag = mp3.GetTag (Id3TagFamily.FileStartTag);
				
				if(tag != null){
					
					/*
					Debug.Log ("Title: " + tag.Title.Value);
					Debug.Log ("Artist: " + tag.Artists.Value);
					Debug.Log ("Album: " + tag.Album.Value);
					Debug.Log("Genre: " + tag.Genre.Value);
*/
					//Debug.Log(tag.Artist);

					PlaylistItem newest = new PlaylistItem(musicFile,tag.Genre, tag.Artist, tag.Album, tag.Title);
					Debug.Log (newest.toString());
					songpool.Add(newest);
					Stream stream = new FileStream("songpool/"+songpool.Count+".bin", FileMode.Create, FileAccess.Write, FileShare.None);
					formatter.Serialize(stream, newest);
					stream.Close();
				}			
				
				
				
			//}
		}
		
		
	}
	
	public void load(){
		
		// *Get the size of the songpool by getting the largest no. bin file. Afterwards intialize a new songpool array and fill it with the deserialized bin files.
		string[] spItems = Directory.GetFiles ("songpool/","*.*");
		foreach(string s in spItems){
			Stream stream = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.Read);
			PlaylistItem temp = (PlaylistItem) formatter.Deserialize(stream);
			songpool.Add(temp);
			stream.Close();
			
		}
	}

	public void sortArtists(){
		foreach(PlaylistItem pl in songpool){
			bool temp = false;
			foreach(Artist a in artists){
				if(a.name == pl.artist){
					temp = true;
					a.songs.Add(pl);
				}
			}
			if(temp == false){
				artists.Add(new Artist(pl.artist,pl.genre));
				//Song needs to be added for the artist
			}
		}
		foreach(Artist a in artists){
			Debug.Log (a.name);
		}
	}
}

public class Artist : System.Object{
	public string name;
	public string genre;
	public List<PlaylistItem> songs = new List<PlaylistItem>();

	public Artist(string tname, string tgenre){
		name = tname;
		genre = tgenre;

	}
}
