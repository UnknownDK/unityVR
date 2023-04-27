using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clean : MonoBehaviour
{
    double[,] imageSovsRen;
    int sovsIndex;
    string[] wallReflectsRen; 
    
    public void CleanUpImageSources(double[,] imageSovsUren, int supposedSize, string[] wallReflectsUren)
    {   
        imageSovsRen = new double[supposedSize, imageSovsUren.GetLength(1)];
        wallReflectsRen = new string[supposedSize];
        sovsIndex = 0;
        for (int i = 0; i < imageSovsUren.GetLength(0); i++)
        {
            if(!Double.IsNaN(imageSovsUren[i,0]))
            {
                for (int j = 0; j < imageSovsUren.GetLength(1); j++)
                {
                    imageSovsRen[sovsIndex,j] = imageSovsUren[i,j];
                }
                wallReflectsRen[sovsIndex] = wallReflectsUren[i];
                sovsIndex++;
            }
        }
    }
    public double[,] getCleanImageSources()
    {
        return imageSovsRen;
    }

    public string[] getWallsReflectedOn()
    {
        return wallReflectsRen;
    }
}
