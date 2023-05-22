using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFilterCircBuff {
    private static float[] internalBuffer = new float[30];
    private static float[] internalBuffer2 = new float[59];
    private static int newBufferIndex = 0;
    
    public float applyFilter(float input, float[] coefficients) {
        float output = 0f;

        if (coefficients.Length > 30 )
        {
            internalBuffer2[newBufferIndex] = input;
            int m = internalBuffer2.Length;
            for (int i = 0; i < m; i++) 
            {
                output += internalBuffer2[(newBufferIndex + m - i) % m]*coefficients[i];
            }
            newBufferIndex = (newBufferIndex + 1) % internalBuffer2.Length;
        } 
        else
        {
            internalBuffer[newBufferIndex] = input;
            int m = internalBuffer.Length; 
            for (int i = 0; i < m; i++) 
            {
                output += internalBuffer[(newBufferIndex + m - i) % m]*coefficients[i];
            }
            newBufferIndex = (newBufferIndex + 1) % internalBuffer.Length;
        }
        
        return output;
    }
}

