﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkIt;

public class BodyTemplate : MonoBehaviour {

    public List<BodyGameObject> bodies = new List<BodyGameObject>();
    private MeshRenderer mesh;
    public GameObject[] calvinObjects;
    public GameObject[] debbieObjects;

    void Start () {
        mesh = GetComponent<MeshRenderer>();
		mesh.enabled = true;
//        mesh.material.color = new Color(1.0f, 0.0f, 1.0f);

        calvinObjects = GameObject.FindGameObjectsWithTag("Calvin");
//        debbieObjects = GameObject.FindGameObjectsWithTag("Debbie");

//        if (calvinObjects.Length == 0)
//        {
//            Debug.Log("No game objects are tagged with Calvin");
//        } else
//        {
//            Debug.Log("Calvin found " + calvinObjects.Length);
//            foreach (GameObject g in calvinObjects)
//            {
//                Debug.Log(g.ToString());
//                g.GetComponent<Renderer>().enabled = !g.GetComponent<Renderer>().enabled;
//            }
//        }
//
//
//
//        if (debbieObjects.Length == 0)
//        {
//            Debug.Log("No game objects are tagged with Debbie");
//        }
//        else
//        {
//            Debug.Log("Debbie found " + debbieObjects.Length);
//            foreach (GameObject g in debbieObjects)
//            {
//                Debug.Log(g.ToString());
//                g.GetComponent<Renderer>().enabled = !g.GetComponent<Renderer>().enabled;
//            }
//        }
    }


    //wait for KinectManager to completely update first
    void LateUpdate () {
        //TODO Your code here
//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            showPaused();
//        }
        if (bodies.Count > 0)
        {
			showCalvin ();
            //some bodies, send orientation update
            GameObject thumbRight = bodies[0].GetJoint(Windows.Kinect.JointType.ThumbRight);
            GameObject handRight = bodies[0].GetJoint(Windows.Kinect.JointType.HandRight);
            GameObject handTipRight = bodies[0].GetJoint(Windows.Kinect.JointType.HandTipRight);

            float wristRotation = KinectCVUtilities.VerticalWristRotation(
                handTipRight.transform.localPosition,
                handRight.transform.localPosition,
                thumbRight.transform.localPosition
                );

            //update the rotation
            this.transform.rotation = Quaternion.Euler(0, wristRotation, 0);

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
