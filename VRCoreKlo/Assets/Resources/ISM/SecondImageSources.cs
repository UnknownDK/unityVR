using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecondImageSources
{
    private double[,] imageSources;
    private int noOfCoords;
    private int noOfProjs;
    private int noOfOldSovser;
    private double[,] imageDummy;
    private double[] checkSumSovs;
    private double[] dotNormalPointvector;
    private double[,] pointVectors;
    private double[,] projection;
    private string[] wallsReflectedOn;
    
    public void SecondImageSourcesFun(double[,] firstImages, double[,] wallNormals, double[,,] wallVertices, double[] point, string[] firstWallReflects) 
    {
        noOfCoords = firstImages.GetLength(1); //X, Y, Z
        noOfProjs = wallNormals.GetLength(0); //Antal normaler, vi har
        noOfOldSovser = firstImages.GetLength(0);

        List<double> checkSum = new List<double>();
        checkSumSovs = new double[noOfOldSovser*noOfProjs]; 
        for (int j = 0; j < noOfCoords; j++) // lægger OG kilde ind
        {
            checkSumSovs[0] += Math.Round(arbitraryFunction(point[j], j),3);
        }
        checkSum.Add(checkSumSovs[0]);

        checkSumSovs = new double[noOfOldSovser*noOfProjs]; // nulstil
        for (int o = 0; o < noOfOldSovser; o++) // Tilføj gamle kilder
        {
            for (int j = 0; j < noOfCoords; j++)
            {
                checkSumSovs[o] += Math.Round(arbitraryFunction(firstImages[o, j], j),3);
            }
            //Console.WriteLine("GAMLE CHECKSUMS: {0}", checkSumSovs[o]);
            checkSum.Add(checkSumSovs[o]);
        }

        checkSumSovs = new double[noOfOldSovser*noOfProjs]; // nulstil
        imageSources = new double[noOfOldSovser*noOfProjs, noOfCoords]; // Den skal evaluere 36 
        imageDummy = new double[noOfOldSovser*noOfProjs, noOfCoords]; // Holder kilderne, før der bliver tjekket, om de allerede er i listen
        wallsReflectedOn = new string[noOfOldSovser*noOfProjs];

        ////////////// HER BEGYNDER SELVE ISM
        int y = 0; // indeksering
        int sovserAdded = 0;
        for (int o = 0; o < noOfOldSovser; o++)
        {
            //Nulstiller disse for hver sovs
            pointVectors = new double[noOfProjs, noOfCoords];
            dotNormalPointvector = new double[noOfProjs];

            for (int q = 0; q < noOfProjs; q++)
            {
                for (int j = 0; j < noOfCoords; j++)
                {
                    pointVectors[q, j] = firstImages[o, j] - wallVertices[q, 0, j]; 
                }
                dotNormalPointvector[q] = pointVectors[q, 0] * wallNormals[q, 0] +
                                      pointVectors[q, 1] * wallNormals[q, 1] + 
                                      pointVectors[q, 2] * wallNormals[q, 2] ;
                if (dotNormalPointvector[q] <= 0) //Flipper fortegnet, hvis vi finder normalen på den forkerte side
                {
                    for (int j = 0; j < noOfCoords; j++)
                    {
                        wallNormals[q, j] *= -1;
                        //dotNormalPointvector[q] *= -1; // hvorfor gør vi det her?
                    }
                }
            } 

            Projection projections = new Projection(wallNormals, pointVectors);
            projection = projections.GetProjections();

            ///////////////////////////////// STADIG INDE I noOfOldSovser-LOOPET!!!!

            for (int q = 0; q < noOfProjs; q++)
            {
                for (int j = 0; j < noOfCoords; j++)
                {
                    imageDummy[y, j] = Math.Round(firstImages[o, j] - 2 * projection[q, j],3);
                    checkSumSovs[y] += Math.Round(arbitraryFunction(imageDummy[y, j], j),3);
                }
                if (!imageContainedCheck(checkSumSovs[y], checkSum))
                {
                    sovserAdded++;
                    //Console.WriteLine("Sovser tilføjet: {0}", sovserAdded);
                    for(int j = 0; j < noOfCoords; j++)
                    {
                        imageSources[y, j] = imageDummy[y, j];
                    }
                    checkSum.Add(checkSumSovs[y]);
                    wallsReflectedOn[y] = o.ToString() + " -> " + q.ToString();
                }
                else
                {
                    //Console.WriteLine("IMAGE SOURCE ALREADY CONTAINED!!");
                    for(int j = 0; j < noOfCoords; j++)
                    {
                        imageSources[y, j] = Double.NaN;
                    }
                }
                //Console.WriteLine(wallsReflectedOn[y]);
                y++;
            }  
        } // FOR-LOOP MED noOfOldSovser
        Clean clean = new Clean();
        clean.CleanUpImageSources(imageSources, sovserAdded, wallsReflectedOn);
        imageSources = clean.getCleanImageSources();
        wallsReflectedOn = clean.getWallsReflectedOn();
    } 

    public double[,] GetSecondImageSources() {
        return imageSources;
    }

    public string[] GetSecondReflects() {
        return wallsReflectedOn;
    }
    public bool imageContainedCheck(double checkSumSovs, List<double> checkSum){ // Det her behøver ikke være en funktion for sig selv, men er et levn
        if(checkSum.Contains(checkSumSovs))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public double arbitraryFunction(double funken, int iteratorJ)
    {
        if(iteratorJ == 0)
        {
            return (funken*(double)181); // primtal
        }
        else if(iteratorJ == 1)
        {
            return (funken*(double)389);
        }
        else if(iteratorJ == 2)
        {
            return (funken*(double)137);
        }
        else
        {
            return Double.NaN;
        }
    }
}