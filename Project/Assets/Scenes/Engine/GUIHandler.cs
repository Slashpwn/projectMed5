using UnityEngine;
using System.Collections;

public class GUIHandler : MonoBehaviour {
bool isPlaying = false;
	
	public void playPause () {
		if(isPlaying == true)
			playMusic(); 
		else pauseMusic(); 
	}
	
	public void playMusic () {
		isPlaying = true;
		Debug.Log("Playing Song");
	}
	
	public void pauseMusic () {
		isPlaying = false;
		Debug.Log("Pause Song");
	}
	public void nextSong () {
		Debug.Log("Next Song");

	}
	public void previousSong () {
		Debug.Log("Previous Song");
	}
}
