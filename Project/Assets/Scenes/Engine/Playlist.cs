using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playlist : MonoBehaviour {
	public ArrayList PLI;
	public List<PlaylistItem> PL = new List<PlaylistItem>();
	public string title;
	public int length;
	public int lastPlay;

	public Playlist(string name){
		this.name = name;
	
	}

	void add(PlaylistItem item){
		PL.Add (item);
		length++;

	}

	void remove(PlaylistItem item){
		PL.Remove (item);
		length--;

	}

	int getNoOfItems(){
		return length;

	}

	public PlaylistItem getNext(int count){
		if (count + 1 < length) {
						return PL [count + 1];
				} else {
			return PL[length-1]		;
		}
	}

	public PlaylistItem getPrev(int count){
		if (count - 1 >= 0) {
						return PL [count - 1];
				} else {
			return PL[0];		
		}
	}

}
