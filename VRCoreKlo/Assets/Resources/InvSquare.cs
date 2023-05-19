using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InvSquare : MonoBehaviour
{   
    public float dist = 0.0f;
    //private float[] camPos = new float[3] {0, 0, 0};
    //private float[] sourcePos = new float[3] {0, 0, 0};
    bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        //get listener position, which is the same as camera position
	// get listener position, which is the same as camera position 

        // calculate distance in meters
        dist = Vector3.Distance(transform.position, Camera.main.transform.position);

        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        //get listener position, which is the same as camera position
        //get listener position, which is the same as camera position
	// get listener position, which is the same as camera position 
	//camPos = Camera.main.transform.position;

	// get source position 
	//sourcePos = transform.position;


        // calculate distance in meters
        // dist = (float)Math.Sqrt(Math.Pow(camPos[0]-sourcePos[0], 2) + Math.Pow(camPos[1]-sourcePos[1], 2) + Math.Pow(camPos[2] - sourcePos[2], 2));
        dist = Vector3.Distance(transform.position, Camera.main.transform.position);
        //print(dist.ToString() + " " + camPos[0].ToString() + " " + camPos[1].ToString() + " " + camPos[2].ToString() + " " + sourcePos[0].ToString() + " " + sourcePos[1].ToString() + " " + sourcePos[2].ToString());
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if(!ready)
        {
            return;
        }
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = data[i] / (dist * dist);
        }
    }
}
