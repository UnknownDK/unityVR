using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class playDelay : MonoBehaviour
{
    float[] buffer = new float[10000];
    float[] saveBuffer = new float[4096];
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
    }


    void OnAudioFilterRead (float[] data, int channels) 
    {
        

        for (int i = 0; i < data.Length/2; i += 2)
        {
            data[i*2] = buffer[writePos];
            data[i*2 + 1] = buffer[writePos + 1];

            writePos += 2;  
            if(writePos > buffer.Length - data.Length - 1000) // same as above
            {
                writePos = 0;
            }
        }
        
        
        /*
        for (int i = 0; i < newSound.Length; i += 1 ) 
        {
            // put output sample into buffer and then replace with correct sample, based on distance between listener and source
            buffer[readPos] = newSound[i];
            data[i] = buffer[writePos];

            readPos++;
            writePos++;
            if(readPos > buffer.Length - 1000) // -1000 to ensure that diffOffset + data.Length is inside buffer size
            {
                readPos = 0;
            }
            if(writePos > buffer.Length - 1000)
            {
                writePos = 0;
            }
        }
        */
        /*
        for (int i = 0; i < data.Length; i += 1 ) 
        {
            // put output sample into buffer and then replace with correct sample, based on distance between listener and source
            buffer[readPos] = data[i];
            data[i] = buffer[writePos];

            readPos++;
            writePos++;
            if(readPos > buffer.Length - 1000) // -1000 to ensure that diffOffset + data.Length is inside buffer size
            {
                readPos = 0;
            }
            if(writePos > buffer.Length - 1000)
            {
                writePos = 0;
            }
        }
        */
    }
}
