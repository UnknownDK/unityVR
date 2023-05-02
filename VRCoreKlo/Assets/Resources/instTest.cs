using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class instTest : MonoBehaviour
{
    public static double[,] ISMPositions;
    public static double[] originalPos;

    Vector3 startPos = new Vector3(1f,0f,1f);
    public static int numSource = 0;
    public static GameObject[] ISMAudioSource;
    public static GameObject originalAudioSource;
    float delayTime = 3f;
    float nextTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        reflectiveSurfaces.Main();
        ISMPositions = reflectiveSurfaces.GetISMs();
        numSource = ISMPositions.GetLength(0);
        originalPos = reflectiveSurfaces.point;

        // load audio clip to be played
        AudioClip clip = (AudioClip)Resources.Load("dabbror");

        
        // instantiate original source 
        GameObject OriginalAudio = (GameObject) Resources.Load("OriginalAudio");
        originalAudioSource = new GameObject();
        originalAudioSource = Instantiate(OriginalAudio);
        originalAudioSource.name = "original";
        originalAudioSource.transform.position = new Vector3((float)originalPos[0], (float)originalPos[2], (float)originalPos[1]);
        originalAudioSource.GetComponent<AudioSource>().clip = clip;
        

        // instantiate ISM sources, change position and attatch sound clip
        GameObject ISMAudio = (GameObject) Resources.Load("ISMAudio"); 
        ISMAudioSource = new GameObject[numSource];
        for (int i = 0; i < numSource; i++)
        {
            ISMAudioSource[i] = Instantiate(ISMAudio);
            ISMAudioSource[i].transform.position = new Vector3((float)ISMPositions[i,0], (float)ISMPositions[i,2], (float)ISMPositions[i,1]);
            ISMAudioSource[i].GetComponent<AudioSource>().clip = clip;
            ISMAudioSource[i].name = i.ToString();
        }

        nextTime = (float)AudioSettings.dspTime + delayTime;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        reflectiveSurfaces.point[0] = 1f+2*Math.Cos(AudioSettings.dspTime);
        reflectiveSurfaces.point[2] = 1f;
        reflectiveSurfaces.point[1] = 2f+2*Math.Sin(AudioSettings.dspTime);

        reflectiveSurfaces.Main();
        ISMPositions = reflectiveSurfaces.GetISMs();
        originalPos = reflectiveSurfaces.point;
        originalAudioSource.transform.position = new Vector3((float)originalPos[0], (float)originalPos[2], (float)originalPos[1]);
        for (int i = 0; i < numSource; i++)
        {
            ISMAudioSource[i].transform.position = new Vector3((float)ISMPositions[i,0], (float)ISMPositions[i,2], (float)ISMPositions[i,1]);
        }
        */
        
        // play sound if sound source is not playing
       if(nextTime < AudioSettings.dspTime)
        {
            originalAudioSource.GetComponent<AudioSource>().Play();

            for(int i = 0; i < numSource; i++)
            {
                ISMAudioSource[i].GetComponent<AudioSource>().Play();
                //Debug.Log("play");
            }
            nextTime += delayTime;
        }

    }

    public void WriteString(string s)
    {
        string path = Application.persistentDataPath + "/buffer.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
    }
}
