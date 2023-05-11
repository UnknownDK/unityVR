using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFilterCircBuffDyna {
    private static float[] internalBuffer;
    private static int newBufferIndex = 0;

    public void setFilterLength(int filterLength) {
        internalBuffer = new float[filterLength];
    }
    
    public float applyFilter(float input, float[] coefficients) {
        float output = 0f;
        internalBuffer[newBufferIndex] = input;
        for (int i = 0; i < internalBuffer.Length; i++) 
        {
            output += internalBuffer[(newBufferIndex + i) % internalBuffer.Length]*coefficients[i];
        }
        newBufferIndex = (newBufferIndex + 1) % internalBuffer.Length;        
        return output;
    }
}

