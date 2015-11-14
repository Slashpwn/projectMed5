using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
	bool freeMove = false;
	string focus = "Songpool";
	float lerper = 0;
	float speed = 0.5f;
	Vector3 prevV, newV;
	GameObject cam;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("MainCAM");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("a")){
			fix ();
		}
		if(Input.GetKeyUp("s")){
			free ();
		}


		lerper += speed * Time.deltaTime;
		if (!freeMove) {
			switch(focus){
			case "Playlists":
				break;
			case "Songpool":
				newV = new Vector3(0f, 2.467103f, -6.487131f);
				break;
			}	
			cam.transform.position = Vector3.Lerp (prevV, newV, lerper);
		}

	}

	public void free(){
		freeMove = true;
	}

	public void fix(){
		freeMove = false;
		prevV = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		lerper = 0;
	}

	public void setFocus(string dir){
		prevV = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		}

	public void setManualFocus(float tx, float ty, float tz){
		free ();
		prevV = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		newV = new Vector3 (tx, ty, tz);
		
	}

	public void setSpeed(float temp){
		if (temp < 0) {
						Debug.LogError ("LERP SPEED MUST BE POSITIVE!");
				} else {
			speed = temp;		
		}
	}
}
