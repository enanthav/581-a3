using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkIt;

public class Behaviour : MonoBehaviour {

	public List<BodyGameObject> bodies = new List<BodyGameObject>();
//	private MeshRenderer mesh;
	public GameObject[] calvinObjects;
	public GameObject[] debbieObjects;
	public GameObject[] debbieMail;
	public GameObject[] calvinMail;

	void Start () {
		calvinObjects = GameObject.FindGameObjectsWithTag("Calvin");
		Debug.Log("Calvin found " + calvinObjects.Length);
        debbieObjects = GameObject.FindGameObjectsWithTag("Debbie");
		// personal objects
		calvinObjects [0].SetActive (false);
		debbieObjects [0].SetActive (false);

		// mail objects
		calvinMail = GameObject.FindGameObjectsWithTag("CalvinMailed");
		debbieMail = GameObject.FindGameObjectsWithTag("DebbieMailed");
		calvinMail [0].SetActive (false);
		debbieMail [0].SetActive (false);


		if (calvinObjects.Length == 0) {
			Debug.Log ("No game objects are tagged with Calvin");
		} else {
			foreach (GameObject g in calvinObjects)
			{
				Debug.Log(g.ToString());
			}
		}
		
        if (debbieObjects.Length == 0)
        {
            Debug.Log("No game objects are tagged with Debbie");
        }
        else
        {
            Debug.Log("Debbie found " + debbieObjects.Length);
//            foreach (GameObject g in debbieObjects)
//            {
//                Debug.Log(g.ToString());
//                g.GetComponent<Renderer>().enabled = !g.GetComponent<Renderer>().enabled;
//            }
        }

		Debug.Log ("++++++++Calvin " + calvinObjects [0].activeInHierarchy + " Debbie " + debbieObjects [0].activeInHierarchy);
	}


	//wait for KinectManager to completely update first
	void LateUpdate () {
		if (bodies.Count == 1) {
			Debug.Log ("Bodies count " + bodies.Count);

			calvinObjects [0].SetActive (true);
			debbieObjects [0].SetActive (false);
			Debug.Log ("11111111 Calvin " + calvinObjects [0].activeInHierarchy + " Debbie " + debbieObjects [0].activeInHierarchy);
		} 
		else if (bodies.Count == 2) {
			Debug.Log ("Bodies count " + bodies.Count);
			calvinObjects [0].SetActive (true);
			debbieObjects [0].SetActive (true);

			calvinMail [0].SetActive (true);
			debbieMail [0].SetActive (true);
			Debug.Log ("22222222 Calvin " + calvinObjects [0].activeInHierarchy + " Debbie " + debbieObjects [0].activeInHierarchy);
		}
		else if (bodies.Count == 0 ) {
			Debug.Log ("Bodies count " + bodies.Count);

			debbieObjects [0].SetActive (false);
			calvinObjects [0].SetActive (false);

			calvinMail [0].SetActive (false);
			debbieMail [0].SetActive (false);
		}
	}

	//shows objects with ShowOnPause tag
	void showCalvin()
	{
		foreach (GameObject g in calvinObjects)
		{
			//            g.SetActive(true);
			g.GetComponent<Renderer> ().enabled = !g.GetComponent<Renderer> ().enabled;
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
