using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCanon : MonoBehaviour {
	
	//Rotation
	public float rotationSpeed;
	float xDegrees;
	float yDegrees;
	Quaternion fromRotation;
	Quaternion toRotation;
	Camera camera;

    //Fire Cannon
    public CanonBall cannonBall;
    public Transform shotPos;
    public GameObject explosion;
    CanonBall cannonBallCopy;
    int ballCounter;
    bool hasLeftCannon = true;


    void Start() {
        camera = Camera.main;
    }

    //for key down registration
    void Update() {
        yDegrees = 0;
        xDegrees = 0;
        if (true){
	        if (Input.GetKey(KeyCode.UpArrow)) {
	            yDegrees += rotationSpeed;
	            RotateCannon(); 
	        }
	        if (Input.GetKey(KeyCode.DownArrow)) {
	            yDegrees -= rotationSpeed;
	            RotateCannon(); 
	        }
	        if (Input.GetKey(KeyCode.RightArrow)) {
	            xDegrees += rotationSpeed;
	            RotateCannon();
	        }
	        if (Input.GetKey(KeyCode.LeftArrow)) {
	            xDegrees -= rotationSpeed;
	            RotateCannon(); 
	        }
	        //reset rotation
	        if (Input.GetKeyDown(KeyCode.Backspace)) {
	            shotPos.rotation = Quaternion.Euler(0, 0, 0);
	        }
	        //set to 45
	        if (Input.GetKeyDown(KeyCode.Alpha1)) {
	            shotPos.rotation = Quaternion.Euler(0, 0, 45);
	        }
	        //set to 60
	        if (Input.GetKeyDown(KeyCode.Alpha2)) {
	            shotPos.rotation = Quaternion.Euler(0, 0, 60);
	        }

	        //shoot cannon
	        if (Input.GetKeyDown(KeyCode.Space)) {
	            FireCannon();
	        }
    	}
    }

    public void RotateCannon() {
        shotPos.Rotate(0,xDegrees,yDegrees);
    }

    //for physic stuff
    void FixedUpdate() {
        if (cannonBallCopy != null) {
        	hasLeftCannon = cannonBallCopy.getHasLeftCannon();
        }
    }

    public void FireCannon() {
        cannonBallCopy = Instantiate(cannonBall, shotPos.position, shotPos.rotation) as CanonBall;
        
        Instantiate(explosion, shotPos.position, shotPos.rotation);
        print("shot cannonball " + ++ballCounter);
    }
}




















