using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirFilter {
    //private static float 
    private float[] _coefficients;
    private static float[][] filterCoefficients;

    public float[] FirFilterFunc(int[] wallsHit) 
    {
        FilterCoeffs.assignFilterO1131();
        filterCoefficients = FilterCoeffs.coefficients;
        if (wallsHit[1] < 0)
        {
            _coefficients = filterCoefficients[wallsHit[0]];
        }
        else
        {
            _coefficients = Convolve.Convo(filterCoefficients[wallsHit[0]], filterCoefficients[wallsHit[1]]);
        }
        return _coefficients;
    }
}



