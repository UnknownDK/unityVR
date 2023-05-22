using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFilterCircBuffLoop4 {
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
            float output3 = 0f;
            float output4 = 0f;
            internalBuffer2[newBufferIndex] = input;
            int m = internalBuffer2.Length;
            for (int i = 0; i < m; i+=4) 
            {
                output1 += internalBuffer2[(newBufferIndex + i) % internalBuffer2.Length]*coefficients[i];
                if(i < m-3)
                {
                    output2 += internalBuffer2[(newBufferIndex + i + 1) % internalBuffer2.Length]*coefficients[i + 1];
                    output3 += internalBuffer2[(newBufferIndex + i + 2) % internalBuffer2.Length]*coefficients[i + 2];
                    output4 += internalBuffer2[(newBufferIndex + i + 3) % internalBuffer2.Length]*coefficients[i + 3];
                }
                else if(i < m-2)
                {
                    output2 += internalBuffer2[(newBufferIndex + i + 1) % internalBuffer2.Length]*coefficients[i + 1];
                    output3 += internalBuffer2[(newBufferIndex + i + 2) % internalBuffer2.Length]*coefficients[i + 2];
                }
                else if(i < m-1)
                {
                    output2 += internalBuffer2[(newBufferIndex + i + 1) % internalBuffer2.Length]*coefficients[i + 1];
                }
            }
            newBufferIndex = (newBufferIndex + 1) % internalBuffer2.Length;
            return output1 + output2 + output3 + output4;
        } 
        else
        {
            float output1 = 0f;
            float output2 = 0f;
            float output3 = 0f;
            float output4 = 0f;
            internalBuffer[newBufferIndex] = input;
            int m = internalBuffer.Length; 
            for (int i = 0; i < m; i+=4) 
            {
                output1 += internalBuffer[(newBufferIndex + i) % internalBuffer.Length]*coefficients[i];
                if(i < m-3)
                {
                    output2 += internalBuffer[(newBufferIndex + i + 1) % internalBuffer.Length]*coefficients[i + 1];
                    output3 += internalBuffer[(newBufferIndex + i + 2) % internalBuffer.Length]*coefficients[i + 2];
                    output4 += internalBuffer[(newBufferIndex + i + 3) % internalBuffer.Length]*coefficients[i + 3];
                }
                else if(i < m-2)
                {
                    output2 += internalBuffer[(newBufferIndex + i + 1) % internalBuffer.Length]*coefficients[i + 1];
                    output3 += internalBuffer[(newBufferIndex + i + 2) % internalBuffer.Length]*coefficients[i + 2];
                }
                else if(i < m-1)
                {
                    output2 += internalBuffer[(newBufferIndex + i + 1) % internalBuffer.Length]*coefficients[i + 1];
                }
            }
            newBufferIndex = (newBufferIndex + 1) % internalBuffer.Length;
            return output1 + output2 + output3 + output4;
        }
    }
}

