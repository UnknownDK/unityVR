using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReflectionFilter_revamp
{
    private float[] coefficients;
    private float[] internalBuffer;
    private int bufferIndex;


    public ReflectionFilter_revamp(float[] filtercoefficients)
    {
        coefficients = filtercoefficients;
        internalBuffer = new float[filtercoefficients.Length];   // buffer of size N
        bufferIndex = 0;
    }

    public float ProcessSample(float input)
    {
        internalBuffer[bufferIndex] = input;                                                      // x(n)

        // calculate output sample, reflection filter
        float output = 0f;
        for (int i = 0; i < coefficients.Length; i++)
        {
            output += internalBuffer[(bufferIndex + internalBuffer.Length - i) % internalBuffer.Length] * coefficients[i];
        }

        // increment and wrap buffer indices
        bufferIndex = (bufferIndex + 1) % internalBuffer.Length;

        // return the output sample
        return output;
    }

    public void Reset()
    {
        Array.Clear(internalBuffer, 0, internalBuffer.Length);
        bufferIndex = 0;
    }
}
