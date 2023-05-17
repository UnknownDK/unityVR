using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InverseHeadphones
{
    private float[][] coefficients = new float[2][];
    private float[] internalBuffer;


    public InverseHeadphones (float[] coefs_a, float[] coefs_b)
    {
        coefficients[0] = coefs_a;
        coefficients[1] = coefs_b;
        internalBuffer = new float[coefficients[0].Length + coefficients[1].Length];   // buffer of size N
    }

    public float ProcessSample(float input)
    {
        float output = coefficients[1][0] * input + internalBuffer[0];

        // Update the filter state
        for (int i = 1; i < coefficients[1].Length; i++)
        {
            internalBuffer[i - 1] = coefficients[1][i] * input - coefficients[0][i] * output + internalBuffer[i];
        }

        return output;
    }

    public void Reset()
    {
        Array.Clear(internalBuffer, 0, internalBuffer.Length);
    }
}
