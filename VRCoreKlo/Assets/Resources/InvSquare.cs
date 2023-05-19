using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InvSquare : MonoBehaviour
{   
    public float dist = 0.0f;
    private float[] camPos = new float[3] {0, 0, 0};
    private float[] sourcePos = new float[3] {0, 0, 0};
    // Start is called before the first frame update
    void Start()
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
        dist = (float)MathF.Sqrt(MathF.Pow(camPos[0]-sourcePos[0], 2) + MathF.Pow(camPos[1]-sourcePos[1], 2) + MathF.Pow(camPos[2] - sourcePos[2], 2));
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
        dist = (float)MathF.Sqrt(MathF.Pow(camPos[0]-sourcePos[0], 2) + MathF.Pow(camPos[1]-sourcePos[1], 2) + MathF.Pow(camPos[2] - sourcePos[2], 2));
        //print(dist.ToString() + " " + camPos[0].ToString() + " " + camPos[1].ToString() + " " + camPos[2].ToString() + " " + sourcePos[0].ToString() + " " + sourcePos[1].ToString() + " " + sourcePos[2].ToString());
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (dist != 0)
            {
                data[i] = data[i] / (dist * dist);
            }
        }
    }
}
