using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addDelay : MonoBehaviour
{
    int newOffset = 0;
    int oldOffset = 0;
    int diffOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        oldOffset = CreateSources.zeroOffset;
    }


    // Update is called once per frame
    void Update()
    {
        if(GetComponent<AudioSource>().isPlaying)
        {
            newOffset = delayCalc.delaySourceSamples;
            diffOffset = oldOffset - newOffset;                  
            oldOffset = newOffset; 
            if((GetComponent<AudioSource>().timeSamples + diffOffset) < GetComponent<AudioSource>().clip.samples)
            {
                GetComponent<AudioSource>().timeSamples += diffOffset;
                //Debug.Log(GetComponent<AudioSource>().name + ": " + GetComponent<AudioSource>().timeSamples.ToString());
            }
            else
            {
                GetComponent<AudioSource>().Stop();
                oldOffset = CreateSources.zeroOffset;
                newOffset = 0;
                diffOffset = 0;
            }
        }
        else
        {
            GetComponent<AudioSource>().timeSamples = 0;
            GetComponent<AudioSource>().Stop();
            oldOffset = CreateSources.zeroOffset;
            newOffset = 0;
            diffOffset = 0;
        }
    }
}
