using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFilterNoOptNew {
    private static float[] internalBuffer;

    public void setFilterLength(int filterLength) {
        internalBuffer = new float[filterLength];
    }

    public float applyFilter(float input, float[] coefficients) {
        float output = 0f;

        for (int i = 1; i < internalBuffer.Length; i++ )
        {
            internalBuffer[internalBuffer.Length-i] = internalBuffer[internalBuffer.Length-i-1];
        }
        internalBuffer[0] = input;


        int m = internalBuffer.Length; 
        for (int i = 0; i < m; i++) 
        {
            output += internalBuffer[i]*coefficients[i];
        }

        return output;
    }
}

