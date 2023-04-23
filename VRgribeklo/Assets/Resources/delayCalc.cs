using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class delayCalc : MonoBehaviour
{
    double[] camPos = new double[3] {0, 0, 0};
    double[] sourcePos = new double[3] {0, 0, 0};
    public static int delaySourceSamples = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get listener position, which is the same as camera position
        camPos[0] = Camera.main.transform.position.x;
        camPos[1] = Camera.main.transform.position.y;
        camPos[2] = Camera.main.transform.position.z;

        // get source position 
        sourcePos[0] = transform.position.x;
        sourcePos[1] = transform.position.y;
        sourcePos[2] = transform.position.z;
        
        delaySourceSamples = getDelay(camPos, sourcePos); // calculate sample delay
        //instTest.WriteString(delaySourceSamples.ToString()); // save sample delay to file
    }


    // function used to calculate number of samples the sound should be delayed, based on position of listener ad source
    int getDelay(double[] pos1, double[] pos2) 
    {
        double dist = 0.0;
        double time = 0.0;
        int delay = 0;
        double soundSpeed = 343.2;
        double sampleRate = 44100.0;

        // calculate distance in meters and the time in seconds
        dist = Math.Sqrt(Math.Pow(pos1[0]-pos2[0], 2) + Math.Pow(pos1[1]-pos2[1], 2) + Math.Pow(pos1[2] - pos2[2], 2));
        time = dist/soundSpeed;
        // calculate delay in number of samples, multiply with 2 because there are 2 samples (left and right) for each time instance 
        delay = (int)Math.Round(time*sampleRate)*2; 

        return delay;
    }
}
