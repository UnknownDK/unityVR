using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class instTest : MonoBehaviour
{
    Vector3 startPos = new Vector3(1f,0f,1f);
    public static int numSource = 10;
    public static GameObject[] sphereTest;
    public AudioClip[] clips;
    float delayTime = 3.5f;
    float nextTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nextTime = (float)AudioSettings.dspTime + delayTime;
        GameObject soundSphere = (GameObject) Resources.Load("testPrefab"); 
        sphereTest = new GameObject[numSource];
        
        // instantiate audio clip array, and load clips
        clips = new AudioClip[numSource];
        for(int i = 0; i< numSource; i++)
        {
            clips[i] = (AudioClip)Resources.Load("dabbror");
        }
        
        // instantiate sound sources, change position and attatch sound clip
        for (int i = 0; i < numSource; i++)
        {
            sphereTest[i] = Instantiate(soundSphere);
            sphereTest[i].transform.position = startPos + i*new Vector3((float)Math.Cos(i*2*Math.PI/numSource),0f,(float)Math.Sin(i*2*Math.PI/numSource));
            sphereTest[i].GetComponent<AudioSource>().clip = clips[i];
            sphereTest[i].name = i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // play sound if sound source is not playing
       if(nextTime < AudioSettings.dspTime)
        {
            for(int i = 0; i < numSource; i++)
            {
                sphereTest[i].GetComponent<AudioSource>().Play();
                Debug.Log("play");
            }
            nextTime += delayTime;
        }
    }

    public static void WriteString(string s)
    {
        string path = Application.persistentDataPath + "/buffer.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
    }
}