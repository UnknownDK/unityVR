using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class recordoutput : MonoBehaviour
{
    // Start is called before the first frame update

    public float[] originalBuffer = new float[44100 * 60];
    public float[] processedBuffer = new float[44100 * 60];

    public int bufferIndex = 0;
    public int bufferLength = 44100 * 60; // a
    public bool printingBool = false;

    void Start()
    {

        bufferIndex = 0;
        printingBool = false;

    }

    // Update is called once per frame
    void Update()
    {
        

        if (bufferIndex >= bufferLength && printingBool == false)
        {
            
            printingBool = true;
            //coroutine = savebuffers();
            StartCoroutine(savebuffers());

        }


    }


    private IEnumerator savebuffers()
    {

        string origpath = Application.persistentDataPath + "/orig.txt";
        string processedpath = Application.persistentDataPath + "/processed.txt";
        //Write some text to the test.txt file
        StreamWriter origwriter = new StreamWriter(origpath, true);
        StreamWriter proswriter = new StreamWriter(processedpath, true);

        for (int i = 0; i < bufferLength; i++)
        {
            origwriter.WriteLine(originalBuffer[i]);
            proswriter.WriteLine(processedBuffer[i]);
        }


        origwriter.Close();
        proswriter.Close();

        yield return null;

    }


    void OnAudioFilterRead(float[] data, int channels)
    {
        for(int i = 0; i < data.Length; i+=1)
        {
            if (bufferIndex < bufferLength) {
                originalBuffer[bufferIndex] = data[i];
            }

            data[i] = data[i] * 0.5f;

            if (bufferIndex < bufferLength) {
                processedBuffer[bufferIndex] = data[i];
            }

            bufferIndex += 1;

        }
    }
}
