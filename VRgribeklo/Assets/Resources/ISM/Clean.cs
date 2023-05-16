using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clean
{
    double[,] imageSovsRen;
    int sovsIndex;
    int[,] wallReflectsRen; 
    
    public void CleanUpImageSources(double[,] imageSovsUren, int supposedSize, int[,] wallReflectsUren)
    {   
        imageSovsRen = new double[supposedSize, imageSovsUren.GetLength(1)];
        wallReflectsRen = new int[supposedSize,2];
        sovsIndex = 0;
        for (int i = 0; i < imageSovsUren.GetLength(0); i++)
        {
            if(!Double.IsNaN(imageSovsUren[i,0]))
            {
                for (int j = 0; j < imageSovsUren.GetLength(1); j++)
                {
                    imageSovsRen[sovsIndex,j] = imageSovsUren[i,j];
                }
                wallReflectsRen[sovsIndex,0] = wallReflectsUren[i,0];
                wallReflectsRen[sovsIndex,1] = wallReflectsUren[i,1];
                sovsIndex++;
            }
        }
    }
    public double[,] getCleanImageSources()
    {
        return imageSovsRen;
    }

    public int[,] getWallsReflectedOn()
    {
        return wallReflectsRen;
    }
}
