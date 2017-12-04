using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkIt;

public class Behaviour : MonoBehaviour {

	public List<BodyGameObject> bodies = new List<BodyGameObject>();
	public GameObject[] calvinObjects;
	public GameObject[] debbieObjects;
	public GameObject[] debbieMail;
	public GameObject[] calvinMail;
	public GameObject[] together;
	public GameObject[] oakTrees;

	public float targetTime = 5.0f;

	void Start () {
		// personal objects
		calvinObjects = GameObject.FindGameObjectsWithTag("Calvin");
        debbieObjects = GameObject.FindGameObjectsWithTag("Debbie");

		// mail objects
		calvinMail = GameObject.FindGameObjectsWithTag("CalvinMailed");
		debbieMail = GameObject.FindGameObjectsWithTag("DebbieMailed");
		calvinMail [0].SetActive (false);
		debbieMail [0].SetActive (false);

		// oak tree counters
		oakTrees = GameObject.FindGameObjectsWithTag("Player");

		// together objects
		together = GameObject.FindGameObjectsWithTag("LaughedTogether");
		together [0].SetActive (false);
		Debug.Log("Together found " + together.Length);
		Debug.Log("Together found " + together[0].ToString());


		if (oakTrees.Length == 0) {
			Debug.Log ("No game objects are tagged with player");
		} else {
			Debug.Log("Calvin found " + oakTrees.Length);
			foreach (GameObject g in oakTrees)
			{
				Debug.Log(g.ToString());
				g.SetActive (false);
			}
		}


		if (calvinObjects.Length == 0) {
			Debug.Log ("No game objects are tagged with Calvin");
		} else {
			Debug.Log("Calvin found " + calvinObjects.Length);
			foreach (GameObject g in calvinObjects)
			{
				Debug.Log(g.ToString());
				g.SetActive (false);
			}
		}
		
        if (debbieObjects.Length == 0)
        {
            Debug.Log("No game objects are tagged with Debbie");
        }
        else
        {
            Debug.Log("Debbie found " + debbieObjects.Length);
            foreach (GameObject g in debbieObjects)
            {
                Debug.Log(g.ToString());
				g.SetActive (false);
            }
        }

		Debug.Log ("++++++++Calvin " + calvinObjects [0].activeInHierarchy + " Debbie " + debbieObjects [0].activeInHierarchy);
	}


	//wait for KinectManager to completely update first
	void LateUpdate () {
		if (bodies.Count == 1) {
//			Debug.Log ("Bodies count " + bodies.Count);

			foreach (GameObject g in calvinObjects)
			{
//				Debug.Log(g.ToString());
				g.SetActive (true);
			}

			foreach (GameObject g in debbieObjects)
			{
//				Debug.Log(g.ToString());
				g.SetActive (false);
			}
			oakTrees [1].SetActive (true);
//			Debug.Log ("11111111 Calvin " + calvinObjects [0].activeInHierarchy + " Debbie " + debbieObjects [0].activeInHierarchy);
		} 
		else if (bodies.Count == 2) {
//			Debug.Log ("Bodies count " + bodies.Count);
			foreach (GameObject g in calvinObjects)
			{
//				Debug.Log(g.ToString());
				g.SetActive (true);
			}

			foreach (GameObject g in debbieObjects)
			{
//				Debug.Log(g.ToString());
				g.SetActive (true);
			}
				
			oakTrees [0].SetActive (true);
//			Debug.Log ("22222222 Calvin " + calvinObjects [0].activeInHierarchy + " Debbie " + debbieObjects [0].activeInHierarchy);
		}
		else if (bodies.Count == 0 ) {
//			Debug.Log ("Bodies count " + bodies.Count);

			foreach (GameObject g in calvinObjects)
			{
//				Debug.Log(g.ToString());
				g.SetActive (false);
			}


			foreach (GameObject g in debbieObjects)
			{
//				Debug.Log(g.ToString());
				g.SetActive (false);
			}

		}

		// *******************************
		// PLACE HOLDER FOR CHEERS!!!!!!!
		// *******************************
		targetTime -= Time.deltaTime;
		if (targetTime <= 0.0f) {
			timerEnded ();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			together [0].SetActive (!together [0].activeInHierarchy);
			calvinMail [0].SetActive (!calvinMail [0].activeInHierarchy);
			debbieMail [0].SetActive (!debbieMail [0].activeInHierarchy);
		}
	}



	void timerEnded() {
		Debug.Log ("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~TIMES UP!!!");
		// number of positive thoughts revealed
		// grow tree
		oakTrees [1].transform.localScale = new Vector3 (3.0f, 3.0f, 3.0f);
		// if the two people are still together for this long...
		if (bodies.Count == 2) {
		// to do... reveal more data or grow tree...


		}
	}

	void Kinect_BodyFound(object args)
	{
		BodyGameObject bodyFound = (BodyGameObject) args;
		bodies.Add(bodyFound);
	}

	void Kinect_BodyLost(object args)
	{
		ulong bodyDeletedId = (ulong) args;

		lock (bodies){
			foreach (BodyGameObject bg in bodies)
			{
				if (bg.ID == bodyDeletedId)
				{
					bodies.Remove(bg);
					return;
				}
			}
		}
	}
}
