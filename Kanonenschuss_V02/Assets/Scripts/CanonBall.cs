using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CanonBall : MonoBehaviour {
	public bool hasGravity = true;
	public float firePower = 1;
	public bool hasAirResistance;
    public string nameOfFile;
	bool hasLeftCannon = false;
	Rigidbody rigidBody;
	private List<List<float>> timeSeries;
	private float currentTimeStep;
    private string myName;
    private bool readyToPrint = true;

    void Start() {
        rigidBody =  GetComponent<Rigidbody>();
        timeSeries = new List<List<float>>();
        myName = nameOfFile;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            WriteTimeSeriesToCSV();
        }
    }

    void FixedUpdate() {

    	if (!hasLeftCannon) {
    		accelerate();
    	}

    	if (hasGravity && hasLeftCannon) {
    		addGravity();
    	}

        if (hasAirResistance && hasLeftCannon) { 
            addAirResistance();
        }

        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<float>() {currentTimeStep, 
            rigidBody.position.x, rigidBody.velocity.y});

        if (currentTimeStep > 20 && readyToPrint) {
            readyToPrint = false;
            WriteTimeSeriesToCSV();
        }

        if (rigidBody.velocity.y < 0.1f && rigidBody.velocity.y > -0) {
            print ("ZENIT; h_max: " + rigidBody.position.y);
        }
    }

    void addGravity() {
       rigidBody.AddForce(new Vector3(0f, -9.81f, 0f), ForceMode.Acceleration);
    }

    void accelerate() {
    	rigidBody.AddForce(transform.right * firePower * 5000);
    }

    void addAirResistance() {
        rigidBody.AddForce(getAirResistance());
    }

    Vector3 getAirResistance() {
        float p = 1.225f;
        //https://en.wikipedia.org/wiki/Drag_coefficient
        float drag = 0.47f;
        float a = Mathf.PI * 0.15f * 0.15f;
        float v = rigidBody.velocity.magnitude;
        float forceAmount = (p * v * v * drag * a) / 2;

        return -rigidBody.velocity.normalized * forceAmount;
    }

    void OnTriggerEnter(Collider other) {
        print("LEFT CANNON; v: " + rigidBody.velocity.magnitude + "; h: " + rigidBody.position.y + "; s: " + rigidBody.position.x);
		if (other.tag == "EndOfCannon") {
			hasLeftCannon = true;
		}
	}

     void OnCollisionEnter(Collision hit) {
        if (hit.gameObject.tag == "Ground") {
            print("LANDED; s: " + rigidBody.position.x + "; h: " + rigidBody.position.y);
        }
    }

    void WriteTimeSeriesToCSV() {
        using (var streamWriter = new StreamWriter(myName + ".csv")) {
            //streamWriter.WriteLine("t,s(t),v(t)");
            
            foreach (List<float> timeStep in timeSeries) {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }

	public bool getHasLeftCannon() {
		return hasLeftCannon;
	}
}
