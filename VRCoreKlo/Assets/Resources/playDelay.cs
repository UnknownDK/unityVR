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
        
        readPos += diffOffset;

        // we split the array into tow new left and right channel arrays
        int leftCount = data.Length / 2; // round up to ensure even count
        int rightCount = data.Length / 2;
        float[] leftArray = new float[leftCount];
        float[] rightArray = new float[rightCount];
        for (int i = 0; i < data.Length; i++)
        {
            if (i % 2 == 0)
            {
                leftArray[i / 2] = data[i];
            }
            else
            {
                rightArray[i / 2] = data[i];
            }
        }

        // we interpolate based on how many samples should be skipped.
        int originalSize = data.Length/2;
        int newSize = originalSize + diffOffset/2;
        float[] newSound = new float[newSize*2];
        // calculate the sample rate for the original and new sounds
        float originalSampleRate = 44100f;
        float newSampleRate = 44100f * ((float)newSize / originalSize);

        // calculate the time step for the original and new sounds
        float originalTimeStep = 1f / originalSampleRate;
        float newTimeStep = 1f / newSampleRate;

        // interpolate the samples in the new sound array
        for (int i = 0; i < newSize; i++)
        {
            float originalIndex = i * originalSize / (float)newSize;
            int index1 = (int)Math.Floor(originalIndex);
            int index2 = (int)Math.Ceiling(originalIndex);

            if (index1 < 0)
            {
                index1 = 0;
            }
            else if (index1 >= originalSize)
            {
                index1 = originalSize - 1;
            }

            if (index2 < 0)
            {
                index2 = 0;
            }
            else if (index2 >= originalSize)
            {
                index2 = originalSize - 1;
            }

            float t = originalIndex - index1;

            float sample1L = leftArray[index1];
            float sample2L = leftArray[index2];
            newSound[i*2] = (1 - t) * sample1L + t * sample2L;

            float sample1R = rightArray[index1];
            float sample2R = rightArray[index2];
            newSound[i*2 + 1] = (1 - t) * sample1R + t * sample2R;

            if(readPos > buffer.Length - data.Length) // should ensure that readPos stays within bounds (does not work for very fast movement)
            {
                readPos = 0;
            }
            else if(readPos < 0)
            {
                readPos = buffer.Length + readPos;
            }
            buffer[readPos] = newSound[i*2];
            buffer[readPos + 1] = newSound[i*2 + 1];            
            readPos += 2;
        }

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
