using UnityEngine;
using System.Collections;



public class Touch : MonoBehaviour {

//For clicking on items
GameObject Object_to_move;
private float timer;

//Current and previous frame vectors 
private Vector2 v2_currentFrame; 
private Vector2 v2_previousFrame;
private Vector2 v2_current; 
private Vector2 v2_previous;
//New vector from start to end vector 
private float newVector; 

// Camera
public Camera myCamera;

//For SmoothDamp: writing .zero is short for writing (0,0,0); 
private Vector3 velocity = Vector3.zero; 
public float smoothTime = 0.3F;

//Zooming 
public float zoomComfortZone;
public byte zoomStrength; 
public byte maxFov; 
public byte minFov;


 
	// Use this for initialization
	void Start () {
		
	}
	
	/*public static Vector2 FixedTouchDelta(Vector2 aTouch)
	{
		float dt = Time.deltaTime / aTouch.deltaTime; 
		if (dt == 0 || float.IsNan(dt) || float.IsInfinity(dt))
			dt = 1.0f; 
		return atouch.deltaPosition * dt; 
	}*/
	
	// Update is called once per frame
	void Update () {
		
		if (Input.touchCount == 3 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			
			myCamera.transform.Rotate(90.0f, 0.0f, 0.0f);
			//Figure out the best way to lerp it with beauty

			Debug.Log("Three fingers");
			
		}

		//Will check if we're touching or not
		if(Input.touchCount == 1) {
			//Debug.Log("Debug: Touch Happened");	
			
			/***********      MOVING CAMERA AND SELECTING    **************

			/*A switch statement to check between 3 conditions: 
			TouchPhase.Began: True if a touch Phase has begun. A timer is created with the Time class.
			TouchPhase.Began: True if tap position has moved. Used for moving the x position of the myCamera 
			variable, by using the transform.Translate(x, y, z). The timer is used to see if there has gone 
			more than 0.1 (seconds??) since touch phase begun. This is done to differentiate between a single
			tap and a move and a slide. 
			TouchPhase.Ended: True if touch phase has ended. If it's ended we know it's a single tap. To 
			select and object we need to send out a ray (theoretically infinite line from a point) from 
			our cameras xy plane, for setting a ray starting position in screen we use the 
			Camera.main.ScreenPointToRay(position of touch). And to check we hit anything with the 
			ray we use the Physics.Raycast(ray, out RaycastHit). If we hit we we 
			can "select" the object by using (hit.transform.name)*/
			switch(Input.GetTouch(0).phase)
			{
				case TouchPhase.Began: 
					timer = Time.time; 

				break; 
				case TouchPhase.Moved: 
					if (Time.time-timer > 0.1F)
					myCamera.transform.Translate(-Input.GetTouch(0).deltaPosition.x*0.05F, 0,0) ;

				break; 
				case TouchPhase.Ended:
				if (Input.GetTouch(0).tapCount == 1) {
						Debug.Log("Single Tap with one Finger");
						 
						RaycastHit hit; 
						Ray ray; 
						
						ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); 
							if(Physics.Raycast(ray, out hit)) {
								
								Debug.Log(hit.transform.name);
								
							}
				}
				break; 
			}
			/*if (Input.GetTouch(0).phase == TouchPhase.Moved) {
				
				timer = Time.timer; 
				/* Tranforming the camera position (always a Vector3). 
				A new vector is created. To give it a smooth movement the Mathf function SmoothDamp is used for the x axis.
				SmoothDamp requires (Vector3 current, vector3 target, ref Vector3 currentVelocity, smoothTime); 
				Our Current vector is just the x position. Our target would be the current position minus the movement of 
				our finger since last frame. Input.GetTouch(0) is the current finger touching the screen, deltaPosition.x is 
				the movement in the x direction since last frame. The current velocity is just 0, read above. And smoothTime 				
				is the approximate time it takes to move from A to B.  If we have performance issues then instead of SmoothDamp 
				we could use Mathf.Lerp. Which is linear interpolation between to variables. 
				*/				
				/*myCamera.transform.position = new Vector3 (Mathf.SmoothDamp(myCamera.transform.position.x, 
																			myCamera.transform.position.x + Input.GetTouch(0).deltaPosition.x, 
																			ref velocity.x, smoothTime),
														myCamera.transform.position.y,
														myCamera.transform.position.z);*/
				
				//if (timer - Time.time >= 0.5)
				//myCamera.transform.Translate(-Input.GetTouch(0).deltaPosition.x*0.1F, 0,0) ;
				
			/*}
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {

				if (Input.GetTouch(0).tapCount == 1) {
						Debug.Log("Single Tap with one Finger");
						 
						RaycastHit hit; 
						Ray ray; 
						
						ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); 
							if(Physics.Raycast(ray, out hit)) {
								
								Debug.Log(hit.transform.name);
								
							}
				}
			}*/
			//****************************************************************************
			//I'll keep this here, since we might use it laster and it's already well-documented
			
			//Asking if the touching has stopped
			/*if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				//set the v2_current vector to the end position of the touch
				v2_current = Input.GetTouch(0).position; 
				//Getting the length between the two vectors
				newVector = v2_current.magnitude - v2_previous.magnitude; 
				//Checking if the the length of touch can be considered a touch
				//Mathf.Abs gives us an absolute value (negative numbers becomes positive)
				if (Mathf.Abs(newVector) > i_comfort) {
					/*Next if, else statements will check if swipe is from right, left, top or bottom
					If the end vector(v2_current) is bigger than the start vector (v2_previous)
					we will either have a movement from the left to right or from the bottom 
					to the top.*/ /*
					if(newVector>0) {
						/*To check whether the swipe is from left to right or bottom to the top, we will
						check if the distance of the x value from the end and start vectors are bigger 
						Than the distance of the y values from the two vectors. If x distance is bigger 
						then we know that its a horizontal swipe. And if not it is a vertical swipe. 
						Notice the use of Mathf.Abs makes sure the arrangement of variables doesn't matter */
						/*if(Mathf.Abs(v2_current.x - v2_previous.x) > Mathf.Abs(v2_current.y - v2_previous.y))
						{
							Debug.Log("Left to right");
							Debug.Log(Mathf.Lerp(myCamera.transform.position.x, myCamera.transform.position.x + newVector, Time.deltaTime*5));
							myCamera.transform.position = new Vector3 
														(Mathf.Lerp(myCamera.transform.position.x, myCamera.transform.position.x + newVector, Time.deltaTime),
														myCamera.transform.position.y,
														myCamera.transform.position.z);
														
							
							
							
						}
						else 
						{
							Debug.Log("Bottom to top");
						}
						
					} else {
						//The opposite of what is calculated above. Checking right to left and top to bottom.
						if (Mathf.Abs(v2_current.x - v2_previous.x) > Mathf.Abs(v2_current.y - v2_previous.y))
						{
							Debug.Log("Right to left"); 
						}
						else 
						{
						Debug.Log("Top to Bottom"); 

						}
					}
				}
			}*/
		}
		/***********      ZOOMING    **************

		To check if to fingers is on the screen we use the "Input.touchCount == 2". First finger is indexed 
		at 0 and the other at 1. If both are moving then we will zoom. 
		*/
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
		{
			/*This is a little confusing but to save performance, instead of assigning and removing new variable
			every frame I put all the calculations into a single variable. 
			1. We create a new vector by subtracting the position of the touch of finger 0 with finger 1 
			This the position of our current frame. To find the vector of our previous frame we subtract 
			the current position of the fingers with the deltaPosition(change in position since last frame)
			2. Then we take the magnitude of each of the new vectors and subtract them to get the difference in
			the distance between the two vectors. */
			
			newVector = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude 
								- ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition)
							   - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)).magnitude;
			
			//OR (worse performance)
			
			//v2_currentFrame = Input.GetTouch(0).position - Input.GetTouch(1).position; 
			//v2_previousFrame = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition)
							   //- (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition); 
			//newVector = v2_currentFrame.magnitude - v2_previousFrame.magnitude;
   
			
							   
			/*Checking if the absolute(- becomes +) distance is more than a preset comfort zone*/
			if (Mathf.Abs(newVector) > zoomComfortZone) {
				/*
				1. If the new vector is bigger than 0, we know the that we're zooming. Since the 
				current frame is bigger than the previous. And if the new vector is less than 0 
				then we're zooming out
				2. fieldOfView is used to make a zoom effect, but we're not actually moving the camera 
				position. We use Mathf.Clamp(value, min, max) to avoid zooming to close or too far out, the 
				function clamps a value between min and max. We use Mathf.Lerp(value current, desired value, 
				t). Lerp is linear interpolation between the to values depending on t, which is in a range of 
				0-1. Time.deltaTime is time in seconds it took to complete last frame, by multiplying it with 
				zoomStrength, the higher zoomStrength the faster we will zoom. Our start value will be the 
				current fieldOfView and the the desired value is the the current fieldOfView + the distance
				between 
				
				*/
				if (newVector > 0)
				{
					//Zoom in 
					//Debug.Log("Zoom In");
					myCamera.fieldOfView = Mathf.Clamp(
								   Mathf.Lerp(myCamera.fieldOfView,
								   myCamera.fieldOfView - Mathf.Abs(newVector) * zoomStrength, 
								   Time.deltaTime * zoomStrength),
								   minFov, maxFov);
				} else {
					//Zoom Out
					//Debug.Log("Zoom Out"); 
					myCamera.fieldOfView = Mathf.Clamp(
								   Mathf.Lerp(myCamera.fieldOfView,
								   myCamera.fieldOfView + Mathf.Abs(newVector) * zoomStrength, 
								   Time.deltaTime * zoomStrength),
								   minFov, maxFov); 
				}
			} else {
				
			}
			
		}
		
	
	}
}
