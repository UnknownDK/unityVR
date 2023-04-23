using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playDelayV2 : MonoBehaviour
{
    float[] buffer = new float[15000];
    int writePos = 0;
    int readPos = 0;
    int newOffset = 0;
    int oldOffset = 0;
    int diffOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // delayCalc.delaySourceSamples is used to find the number of samples that equals the propagation time, 
        // the difference between the old and new  offset is used to find out how much the number of samples should change, based on updated positions.
        newOffset = delayCalc.delaySourceSamples;
        diffOffset = newOffset - oldOffset;
        oldOffset = newOffset; 
        Debug.Log("newOffset: " + newOffset);
    }

    void OnAudioFilterRead (float[] data, int channels) 
    {
        // write new data to end of buffer
        for (int i = 0; i < data.Length; i++)
        {
            buffer[writePos] = data[i];
            writePos += 1;  
            if(writePos == buffer.Length) // if writePos is at the end of the buffer, set it to the beginning
            {
                writePos = 0;
            }
        }

        // read delayed data from buffer
        readPos += diffOffset;
        for(int i = 0; i < data.Length; i++)
        {
            if(readPos < 0) // if readPos is negative, set it to the end of the buffer
            {
                readPos = readPos + buffer.Length;
            }
            if(readPos > buffer.Length-1) // if readPos is at the end of the buffer, set it to the beginning
            {
                readPos = readPos - buffer.Length;
            }
            data[i] = buffer[readPos];
            buffer[readPos] = 0;
            readPos += 1;
            
        }
    }
}
