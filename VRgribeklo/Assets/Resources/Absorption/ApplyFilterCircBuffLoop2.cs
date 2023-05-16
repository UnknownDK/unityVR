using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFilterCircBuffLoop2 {
    private static float[] internalBuffer = new float[30];
    private static float[] internalBuffer2 = new float[59];
    private static int newBufferIndex = 0;
    
    //private static float[] twoFilters;
    //private static float[] coefficients; 
    public float applyFilter(float input, float[] coefficients) {
        if (coefficients.Length > 30 )
        {
            float output1 = 0f;
            float output2 = 0f;
            internalBuffer2[newBufferIndex] = input;
            int m = internalBuffer2.Length;
            for (int i = 0; i < m; i+=2) 
            {
                output1 += internalBuffer2[(newBufferIndex + i) % internalBuffer2.Length]*coefficients[i];
                if(i < m-1)
                {
                    output2 += internalBuffer2[(newBufferIndex + i + 1) % internalBuffer2.Length]*coefficients[i + 1];
                }
            }
            newBufferIndex = (newBufferIndex + 1) % internalBuffer2.Length;
            return output1 + output2;
        } 
        else
        {
            float output1 = 0f;
            float output2 = 0f;
            internalBuffer[newBufferIndex] = input;
            int m = internalBuffer.Length; 
            for (int i = 0; i < m; i+=2) 
            {
                output1 += internalBuffer[(newBufferIndex + i) % internalBuffer.Length]*coefficients[i];
                if(i < m-1)
                {
                    output2 += internalBuffer[(newBufferIndex + i + 1) % internalBuffer.Length]*coefficients[i + 1];
                }
            }
            newBufferIndex = (newBufferIndex + 1) % internalBuffer.Length;
            return output1 + output2;
        }
    }
}

