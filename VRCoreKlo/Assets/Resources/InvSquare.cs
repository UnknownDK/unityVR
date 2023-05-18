using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InvSquare : MonoBehaviour
{
    private float dist = 0.0f;
    private double[] camPos = new double[3] {0, 0, 0};
    private double[] sourcePos = new double[3] {0, 0, 0};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get listener position, which is the same as camera position
        camPos[0] = Camera.main.transform.position.x;
        camPos[1] = Camera.main.transform.position.y;
        camPos[2] = Camera.main.transform.position.z;

        // get source position 
        sourcePos[0] = transform.position.x;
        sourcePos[1] = transform.position.y;
        sourcePos[2] = transform.position.z; 

        // calculate distance in meters
        dist = (float)Math.Sqrt(Math.Pow(camPos[0]-sourcePos[0], 2) + Math.Pow(camPos[1]-sourcePos[1], 2) + Math.Pow(camPos[2] - sourcePos[2], 2));


    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = data[i] / (dist * dist);
        }
    }
}
